    public class ConvertHitObjectParser : Legacy.ConvertHitObjectParser
    {
        public ConvertHitObjectParser(double offset, int formatVersion)
            : base(offset, formatVersion)
        {
        }

        protected override HitObject CreateHit(Vector2 position, bool newCombo, int comboOffset)
        {
            return new ConvertHit();
        }

        protected override HitObject CreateSlider(Vector2 position, bool newCombo, int comboOffset, PathControlPoint[] controlPoints, double? length, int repeatCount,
                                                  List<IList<HitSampleInfo>> nodeSamples)
        {
            return new ConvertSlider
            {
                Path = new SliderPath(controlPoints, length),
                NodeSamples = nodeSamples,
                RepeatCount = repeatCount
            };
        }

        protected override HitObject CreateSpinner(Vector2 position, bool newCombo, int comboOffset, double duration)
        {
            return new ConvertSpinner
            {
                Duration = duration
            };
        }

        protected override HitObject CreateHold(Vector2 position, bool newCombo, int comboOffset, double duration)
        {
            return null;
        }
    }