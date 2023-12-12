        private class PreviewTrackStore : AudioCollectionManager<AdjustableAudioComponent>, ITrackStore
        {
            private readonly IResourceStore<byte[]> store;

            internal PreviewTrackStore(IResourceStore<byte[]> store)
            {
                this.store = store;
            }

            public Track GetVirtual(double length = double.PositiveInfinity)
            {
                if (IsDisposed) throw new ObjectDisposedException($"Cannot retrieve items for an already disposed {nameof(PreviewTrackStore)}");

                var track = new TrackVirtual(length);
                AddItem(track);
                return track;
            }

            public Track Get(string name)
            {
                if (IsDisposed) throw new ObjectDisposedException($"Cannot retrieve items for an already disposed {nameof(PreviewTrackStore)}");

                if (string.IsNullOrEmpty(name)) return null;

                var dataStream = store.GetStream(name);

                if (dataStream == null)
                    return null;

                Track track = new TrackBass(dataStream);
                AddItem(track);
                return track;
            }

            public Task<Track> GetAsync(string name) => Task.Run(() => Get(name));

            public Stream GetStream(string name) => store.GetStream(name);

            public IEnumerable<string> GetAvailableResources() => store.GetAvailableResources();
        }