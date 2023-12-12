    internal class DefaultNotePiece : CompositeDrawable
    {
        public const float NOTE_HEIGHT = 12;

        private readonly IBindable<ScrollingDirection> direction = new Bindable<ScrollingDirection>();
        private readonly IBindable<Color4> accentColour = new Bindable<Color4>();

        private readonly Box colouredBox;

        public DefaultNotePiece()
        {
            RelativeSizeAxes = Axes.X;
            Height = NOTE_HEIGHT;

            CornerRadius = 5;
            Masking = true;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both
                },
                colouredBox = new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Height = NOTE_HEIGHT / 2,
                    Alpha = 0.1f
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load([NotNull] IScrollingInfo scrollingInfo, [CanBeNull] DrawableHitObject drawableObject)
        {
            direction.BindTo(scrollingInfo.Direction);
            direction.BindValueChanged(onDirectionChanged, true);

            if (drawableObject != null)
            {
                accentColour.BindTo(drawableObject.AccentColour);
                accentColour.BindValueChanged(onAccentChanged, true);
            }
        }

        private void onDirectionChanged(ValueChangedEvent<ScrollingDirection> direction)
        {
            colouredBox.Anchor = colouredBox.Origin = direction.NewValue == ScrollingDirection.Up
                ? Anchor.TopCentre
                : Anchor.BottomCentre;
        }

        private void onAccentChanged(ValueChangedEvent<Color4> accent)
        {
            colouredBox.Colour = accent.NewValue.Lighten(0.9f);

            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Colour = accent.NewValue.Lighten(1f).Opacity(0.2f),
                Radius = 10,
            };
        }
    }