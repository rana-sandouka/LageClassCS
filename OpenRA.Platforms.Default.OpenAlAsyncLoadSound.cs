	class OpenAlAsyncLoadSound : OpenAlSound
	{
		static readonly byte[] SilentData = new byte[2];
		readonly CancellationTokenSource cts = new CancellationTokenSource();
		readonly Task playTask;

		public OpenAlAsyncLoadSound(uint source, bool looping, bool relative, WPos pos, float volume, int channels, int sampleBits, int sampleRate, Stream stream)
			: base(source, looping, relative, pos, volume, sampleRate)
		{
			// Load a silent buffer into the source. Without this,
			// attempting to change the state (i.e. play/pause) the source fails on some systems.
			var silentSource = new OpenAlSoundSource(SilentData, SilentData.Length, channels, sampleBits, sampleRate);
			AL10.alSourcei(source, AL10.AL_BUFFER, (int)silentSource.Buffer);

			playTask = Task.Run(async () =>
			{
				MemoryStream memoryStream;
				using (stream)
				{
					try
					{
						memoryStream = new MemoryStream((int)stream.Length);
					}
					catch (NotSupportedException)
					{
						// Fallback for stream types that don't support Length.
						memoryStream = new MemoryStream();
					}

					try
					{
						await stream.CopyToAsync(memoryStream, 81920, cts.Token);
					}
					catch (TaskCanceledException)
					{
						// Sound was stopped early, cleanup the unused buffer and exit.
						AL10.alSourceStop(source);
						AL10.alSourcei(source, AL10.AL_BUFFER, 0);
						silentSource.Dispose();
						return;
					}
				}

				var data = memoryStream.GetBuffer();
				var dataLength = (int)memoryStream.Length;
				var bytesPerSample = sampleBits / 8f;
				var lengthInSecs = dataLength / (channels * bytesPerSample * sampleRate);
				using (var soundSource = new OpenAlSoundSource(data, dataLength, channels, sampleBits, sampleRate))
				{
					// Need to stop the source, before attaching the real input and deleting the silent one.
					AL10.alSourceStop(source);
					AL10.alSourcei(source, AL10.AL_BUFFER, (int)soundSource.Buffer);
					silentSource.Dispose();

					lock (cts)
					{
						if (!cts.IsCancellationRequested)
						{
							// TODO: A race condition can happen between the state check and playing/rewinding if a
							// user pauses/resumes at the right moment. The window of opportunity is small and the
							// consequences are minor, so for now we'll ignore it.
							AL10.alGetSourcei(Source, AL10.AL_SOURCE_STATE, out var state);
							if (state != AL10.AL_STOPPED)
								AL10.alSourcePlay(source);
							else
							{
								// A stopped sound indicates it was paused before we finishing loaded.
								// We don't want to start playing it right away.
								// We rewind the source so when it is started, it plays from the beginning.
								AL10.alSourceRewind(source);
							}
						}
					}

					while (!cts.IsCancellationRequested)
					{
						// Need to check seek before state. Otherwise, the music can stop after our state check at
						// which point the seek will be zero, meaning we'll wait the full track length before seeing it
						// has stopped.
						var currentSeek = SeekPosition;

						AL10.alGetSourcei(Source, AL10.AL_SOURCE_STATE, out var state);
						if (state == AL10.AL_STOPPED)
							break;

						try
						{
							// Wait until the track is due to complete, and at most 60 times a second to prevent a
							// busy-wait.
							var delaySecs = Math.Max(lengthInSecs - currentSeek, 1 / 60f);
							await Task.Delay(TimeSpan.FromSeconds(delaySecs), cts.Token);
						}
						catch (TaskCanceledException)
						{
							// Sound was stopped early, allow normal cleanup to occur.
						}
					}

					AL10.alSourcei(Source, AL10.AL_BUFFER, 0);
				}
			});
		}

		public override void Stop()
		{
			lock (cts)
			{
				StopSource();
				cts.Cancel();
			}

			try
			{
				playTask.Wait();
			}
			catch (AggregateException)
			{
			}
		}

		public override bool Complete
		{
			get { return playTask.IsCompleted; }
		}
	}