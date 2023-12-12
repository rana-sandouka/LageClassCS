    public abstract class ModEasyWithExtraLives : ModEasy, IApplicableFailOverride, IApplicableToHealthProcessor
    {
        [SettingSource("Extra Lives", "Number of extra lives")]
        public Bindable<int> Retries { get; } = new BindableInt(2)
        {
            MinValue = 0,
            MaxValue = 10
        };

        public override string SettingDescription => Retries.IsDefault ? string.Empty : $"{"lives".ToQuantity(Retries.Value)}";

        private int retries;

        private BindableNumber<double> health;

        public override void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            base.ApplyToDifficulty(difficulty);
            retries = Retries.Value;
        }

        public bool PerformFail()
        {
            if (retries == 0) return true;

            health.Value = health.MaxValue;
            retries--;

            return false;
        }

        public bool RestartOnFail => false;

        public void ApplyToHealthProcessor(HealthProcessor healthProcessor)
        {
            health = healthProcessor.Health.GetBoundCopy();
        }
    }