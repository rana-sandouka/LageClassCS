    public class SkinnableDrawable : SkinReloadableDrawable
    {
        /// <summary>
        /// The displayed component.
        /// </summary>
        public Drawable Drawable { get; private set; }

        /// <summary>
        /// Whether the drawable component should be centered in available space.
        /// Defaults to true.
        /// </summary>
        public bool CentreComponent { get; set; } = true;

        public new Axes AutoSizeAxes
        {
            get => base.AutoSizeAxes;
            set => base.AutoSizeAxes = value;
        }

        private readonly ISkinComponent component;

        private readonly ConfineMode confineMode;

        /// <summary>
        /// Create a new skinnable drawable.
        /// </summary>
        /// <param name="component">The namespace-complete resource name for this skinnable element.</param>
        /// <param name="defaultImplementation">A function to create the default skin implementation of this element.</param>
        /// <param name="allowFallback">A conditional to decide whether to allow fallback to the default implementation if a skinned element is not present.</param>
        /// <param name="confineMode">How (if at all) the <see cref="Drawable"/> should be resize to fit within our own bounds.</param>
        public SkinnableDrawable(ISkinComponent component, Func<ISkinComponent, Drawable> defaultImplementation, Func<ISkinSource, bool> allowFallback = null, ConfineMode confineMode = ConfineMode.NoScaling)
            : this(component, allowFallback, confineMode)
        {
            createDefault = defaultImplementation;
        }

        protected SkinnableDrawable(ISkinComponent component, Func<ISkinSource, bool> allowFallback = null, ConfineMode confineMode = ConfineMode.NoScaling)
            : base(allowFallback)
        {
            this.component = component;
            this.confineMode = confineMode;

            RelativeSizeAxes = Axes.Both;
        }

        /// <summary>
        /// Seeks to the 0-th frame if the content of this <see cref="SkinnableDrawable"/> is an <see cref="IFramedAnimation"/>.
        /// </summary>
        public void ResetAnimation() => (Drawable as IFramedAnimation)?.GotoFrame(0);

        private readonly Func<ISkinComponent, Drawable> createDefault;

        private readonly Cached scaling = new Cached();

        private bool isDefault;

        protected virtual Drawable CreateDefault(ISkinComponent component) => createDefault(component);

        /// <summary>
        /// Whether to apply size restrictions (specified via <see cref="confineMode"/>) to the default implementation.
        /// </summary>
        protected virtual bool ApplySizeRestrictionsToDefault => false;

        protected override void SkinChanged(ISkinSource skin, bool allowFallback)
        {
            Drawable = skin.GetDrawableComponent(component);

            isDefault = false;

            if (Drawable == null && allowFallback)
            {
                Drawable = CreateDefault(component);
                isDefault = true;
            }

            if (Drawable != null)
            {
                scaling.Invalidate();

                if (CentreComponent)
                {
                    Drawable.Origin = Anchor.Centre;
                    Drawable.Anchor = Anchor.Centre;
                }

                InternalChild = Drawable;
            }
            else
                ClearInternal();
        }

        protected override void Update()
        {
            base.Update();

            if (!scaling.IsValid)
            {
                try
                {
                    if (Drawable == null || (isDefault && !ApplySizeRestrictionsToDefault)) return;

                    switch (confineMode)
                    {
                        case ConfineMode.ScaleToFit:
                            Drawable.RelativeSizeAxes = Axes.Both;
                            Drawable.Size = Vector2.One;
                            Drawable.Scale = Vector2.One;
                            Drawable.FillMode = FillMode.Fit;
                            break;
                    }
                }
                finally
                {
                    scaling.Validate();
                }
            }
        }
    }