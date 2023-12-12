    [MessagePackObject]
    public class BeatmapAvailability : IEquatable<BeatmapAvailability>
    {
        /// <summary>
        /// The beatmap's availability state.
        /// </summary>
        [Key(0)]
        public readonly DownloadState State;

        /// <summary>
        /// The beatmap's downloading progress, null when not in <see cref="DownloadState.Downloading"/> state.
        /// </summary>
        [Key(1)]
        public readonly float? DownloadProgress;

        [JsonConstructor]
        public BeatmapAvailability(DownloadState state, float? downloadProgress = null)
        {
            State = state;
            DownloadProgress = downloadProgress;
        }

        public static BeatmapAvailability NotDownloaded() => new BeatmapAvailability(DownloadState.NotDownloaded);
        public static BeatmapAvailability Downloading(float progress) => new BeatmapAvailability(DownloadState.Downloading, progress);
        public static BeatmapAvailability Importing() => new BeatmapAvailability(DownloadState.Importing);
        public static BeatmapAvailability LocallyAvailable() => new BeatmapAvailability(DownloadState.LocallyAvailable);

        public bool Equals(BeatmapAvailability other) => other != null && State == other.State && DownloadProgress == other.DownloadProgress;

        public override string ToString() => $"{string.Join(", ", State, $"{DownloadProgress:0.00%}")}";
    }