    public class HitObject
    {
        /// <summary>
        /// A small adjustment to the start time of control points to account for rounding/precision errors.
        /// </summary>
        private const double control_point_leniency = 1;

        /// <summary>
        /// Invoked after <see cref="ApplyDefaults"/> has completed on this <see cref="HitObject"/>.
        /// </summary>
        public event Action<HitObject> DefaultsApplied;

        public readonly Bindable<double> StartTimeBindable = new BindableDouble();

        /// <summary>
        /// The time at which the HitObject starts.
        /// </summary>
        public virtual double StartTime
        {
            get => StartTimeBindable.Value;
            set => StartTimeBindable.Value = value;
        }

        public readonly BindableList<HitSampleInfo> SamplesBindable = new BindableList<HitSampleInfo>();

        /// <summary>
        /// The samples to be played when this hit object is hit.
        /// <para>
        /// In the case of <see cref="IHasRepeats"/> types, this is the sample of the curve body
        /// and can be treated as the default samples for the hit object.
        /// </para>
        /// </summary>
        public IList<HitSampleInfo> Samples
        {
            get => SamplesBindable;
            set
            {
                SamplesBindable.Clear();
                SamplesBindable.AddRange(value);
            }
        }

        [JsonIgnore]
        public SampleControlPoint SampleControlPoint;

        /// <summary>
        /// Whether this <see cref="HitObject"/> is in Kiai time.
        /// </summary>
        [JsonIgnore]
        public bool Kiai { get; private set; }

        /// <summary>
        /// The hit windows for this <see cref="HitObject"/>.
        /// </summary>
        [JsonIgnore]
        public HitWindows HitWindows { get; set; }

        private readonly List<HitObject> nestedHitObjects = new List<HitObject>();

        [JsonIgnore]
        public IReadOnlyList<HitObject> NestedHitObjects => nestedHitObjects;

        public HitObject()
        {
            StartTimeBindable.ValueChanged += time =>
            {
                double offset = time.NewValue - time.OldValue;

                foreach (var nested in NestedHitObjects)
                    nested.StartTime += offset;
            };
        }

        /// <summary>
        /// Applies default values to this HitObject.
        /// </summary>
        /// <param name="controlPointInfo">The control points.</param>
        /// <param name="difficulty">The difficulty settings to use.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void ApplyDefaults(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty, CancellationToken cancellationToken = default)
        {
            ApplyDefaultsToSelf(controlPointInfo, difficulty);

            // This is done here since ApplyDefaultsToSelf may be used to determine the end time
            SampleControlPoint = controlPointInfo.SamplePointAt(this.GetEndTime() + control_point_leniency);

            nestedHitObjects.Clear();

            CreateNestedHitObjects(cancellationToken);

            if (this is IHasComboInformation hasCombo)
            {
                foreach (var n in NestedHitObjects.OfType<IHasComboInformation>())
                {
                    n.ComboIndexBindable.BindTo(hasCombo.ComboIndexBindable);
                    n.IndexInCurrentComboBindable.BindTo(hasCombo.IndexInCurrentComboBindable);
                }
            }

            nestedHitObjects.Sort((h1, h2) => h1.StartTime.CompareTo(h2.StartTime));

            foreach (var h in nestedHitObjects)
                h.ApplyDefaults(controlPointInfo, difficulty, cancellationToken);

            DefaultsApplied?.Invoke(this);
        }

        protected virtual void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            Kiai = controlPointInfo.EffectPointAt(StartTime + control_point_leniency).KiaiMode;

            HitWindows ??= CreateHitWindows();
            HitWindows?.SetDifficulty(difficulty.OverallDifficulty);
        }

        protected virtual void CreateNestedHitObjects(CancellationToken cancellationToken)
        {
            // ReSharper disable once MethodSupportsCancellation (https://youtrack.jetbrains.com/issue/RIDER-44520)
#pragma warning disable 618
            CreateNestedHitObjects();
#pragma warning restore 618
        }

        [Obsolete("Use the cancellation-supporting override")] // Can be removed 20210318
        protected virtual void CreateNestedHitObjects()
        {
        }

        protected void AddNested(HitObject hitObject) => nestedHitObjects.Add(hitObject);

        /// <summary>
        /// Creates the <see cref="Judgement"/> that represents the scoring information for this <see cref="HitObject"/>.
        /// </summary>
        [NotNull]
        public virtual Judgement CreateJudgement() => new Judgement();

        /// <summary>
        /// Creates the <see cref="HitWindows"/> for this <see cref="HitObject"/>.
        /// This can be null to indicate that the <see cref="HitObject"/> has no <see cref="HitWindows"/> and timing errors should not be displayed to the user.
        /// <para>
        /// This will only be invoked if <see cref="HitWindows"/> hasn't been set externally (e.g. from a <see cref="BeatmapConverter{T}"/>.
        /// </para>
        /// </summary>
        [NotNull]
        protected virtual HitWindows CreateHitWindows() => new HitWindows();
    }