    public class CatchDifficultyHitObject : DifficultyHitObject
    {
        private const float normalized_hitobject_radius = 41.0f;

        public new PalpableCatchHitObject BaseObject => (PalpableCatchHitObject)base.BaseObject;

        public new PalpableCatchHitObject LastObject => (PalpableCatchHitObject)base.LastObject;

        public readonly float NormalizedPosition;
        public readonly float LastNormalizedPosition;

        /// <summary>
        /// Milliseconds elapsed since the start time of the previous <see cref="CatchDifficultyHitObject"/>, with a minimum of 40ms.
        /// </summary>
        public readonly double StrainTime;

        public readonly double ClockRate;

        public CatchDifficultyHitObject(HitObject hitObject, HitObject lastObject, double clockRate, float halfCatcherWidth)
            : base(hitObject, lastObject, clockRate)
        {
            // We will scale everything by this factor, so we can assume a uniform CircleSize among beatmaps.
            var scalingFactor = normalized_hitobject_radius / halfCatcherWidth;

            NormalizedPosition = BaseObject.EffectiveX * scalingFactor;
            LastNormalizedPosition = LastObject.EffectiveX * scalingFactor;

            // Every strain interval is hard capped at the equivalent of 375 BPM streaming speed as a safety measure
            StrainTime = Math.Max(40, DeltaTime);
            ClockRate = clockRate;
        }
    }