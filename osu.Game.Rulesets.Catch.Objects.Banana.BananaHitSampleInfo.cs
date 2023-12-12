        private class BananaHitSampleInfo : HitSampleInfo, IEquatable<BananaHitSampleInfo>
        {
            private static readonly string[] lookup_names = { "Gameplay/metronomelow", "Gameplay/catch-banana" };

            public override IEnumerable<string> LookupNames => lookup_names;

            public BananaHitSampleInfo(int volume = 0)
                : base(string.Empty, volume: volume)
            {
            }

            public sealed override HitSampleInfo With(Optional<string> newName = default, Optional<string?> newBank = default, Optional<string?> newSuffix = default, Optional<int> newVolume = default)
                => new BananaHitSampleInfo(newVolume.GetOr(Volume));

            public bool Equals(BananaHitSampleInfo? other)
                => other != null;

            public override bool Equals(object? obj)
                => obj is BananaHitSampleInfo other && Equals(other);

            public override int GetHashCode() => lookup_names.GetHashCode();
        }