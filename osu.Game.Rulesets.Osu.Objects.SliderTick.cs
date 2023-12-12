    public class SliderTick : OsuHitObject
    {
        public int SpanIndex { get; set; }
        public double SpanStartTime { get; set; }

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);

            double offset;

            if (SpanIndex > 0)
                // Adding 200 to include the offset stable used.
                // This is so on repeats ticks don't appear too late to be visually processed by the player.
                offset = 200;
            else
                offset = TimeFadeIn * 0.66f;

            TimePreempt = (StartTime - SpanStartTime) / 2 + offset;
        }

        protected override HitWindows CreateHitWindows() => HitWindows.Empty;

        public override Judgement CreateJudgement() => new SliderTickJudgement();

        public class SliderTickJudgement : OsuJudgement
        {
            public override HitResult MaxResult => HitResult.LargeTickHit;
        }
    }