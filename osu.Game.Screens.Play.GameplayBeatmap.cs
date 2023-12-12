    public class GameplayBeatmap : Component, IBeatmap
    {
        public readonly IBeatmap PlayableBeatmap;

        public GameplayBeatmap(IBeatmap playableBeatmap)
        {
            PlayableBeatmap = playableBeatmap;
        }

        public BeatmapInfo BeatmapInfo
        {
            get => PlayableBeatmap.BeatmapInfo;
            set => PlayableBeatmap.BeatmapInfo = value;
        }

        public BeatmapMetadata Metadata => PlayableBeatmap.Metadata;

        public ControlPointInfo ControlPointInfo
        {
            get => PlayableBeatmap.ControlPointInfo;
            set => PlayableBeatmap.ControlPointInfo = value;
        }

        public List<BreakPeriod> Breaks => PlayableBeatmap.Breaks;

        public double TotalBreakTime => PlayableBeatmap.TotalBreakTime;

        public IReadOnlyList<HitObject> HitObjects => PlayableBeatmap.HitObjects;

        public IEnumerable<BeatmapStatistic> GetStatistics() => PlayableBeatmap.GetStatistics();

        public double GetMostCommonBeatLength() => PlayableBeatmap.GetMostCommonBeatLength();

        public IBeatmap Clone() => PlayableBeatmap.Clone();

        private readonly Bindable<JudgementResult> lastJudgementResult = new Bindable<JudgementResult>();

        public IBindable<JudgementResult> LastJudgementResult => lastJudgementResult;

        public void ApplyResult(JudgementResult result) => lastJudgementResult.Value = result;
    }