	class EchoConnection : IConnection
	{
		protected struct ReceivedPacket
		{
			public int FromClient;
			public byte[] Data;
		}

		readonly List<ReceivedPacket> receivedPackets = new List<ReceivedPacket>();
		public ReplayRecorder Recorder { get; private set; }

		public virtual int LocalClientId
		{
			get { return 1; }
		}

		public virtual ConnectionState ConnectionState
		{
			get { return ConnectionState.PreConnecting; }
		}

		public virtual IPEndPoint EndPoint
		{
			get { throw new NotSupportedException("An echo connection doesn't have an endpoint"); }
		}

		public virtual string ErrorMessage
		{
			get { return null; }
		}

		public virtual void Send(int frame, List<byte[]> orders)
		{
			var ms = new MemoryStream();
			ms.WriteArray(BitConverter.GetBytes(frame));
			foreach (var o in orders)
				ms.WriteArray(o);
			Send(ms.ToArray());
		}

		public virtual void SendImmediate(IEnumerable<byte[]> orders)
		{
			foreach (var o in orders)
			{
				var ms = new MemoryStream();
				ms.WriteArray(BitConverter.GetBytes(0));
				ms.WriteArray(o);
				Send(ms.ToArray());
			}
		}

		public virtual void SendSync(int frame, byte[] syncData)
		{
			var ms = new MemoryStream(4 + syncData.Length);
			ms.WriteArray(BitConverter.GetBytes(frame));
			ms.WriteArray(syncData);
			Send(ms.GetBuffer());
		}

		protected virtual void Send(byte[] packet)
		{
			if (packet.Length == 0)
				throw new NotImplementedException();
			AddPacket(new ReceivedPacket { FromClient = LocalClientId, Data = packet });
		}

		protected void AddPacket(ReceivedPacket packet)
		{
			lock (receivedPackets)
				receivedPackets.Add(packet);
		}

		public virtual void Receive(Action<int, byte[]> packetFn)
		{
			ReceivedPacket[] packets;
			lock (receivedPackets)
			{
				packets = receivedPackets.ToArray();
				receivedPackets.Clear();
			}

			foreach (var p in packets)
			{
				packetFn(p.FromClient, p.Data);
				Recorder?.Receive(p.FromClient, p.Data);
			}
		}

		public void StartRecording(Func<string> chooseFilename)
		{
			// If we have a previous recording then save/dispose it and start a new one.
			Recorder?.Dispose();
			Recorder = new ReplayRecorder(chooseFilename);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				Recorder?.Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}