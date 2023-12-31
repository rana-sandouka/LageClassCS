    public class ScreenSelectionTabControl : OsuTabControl<EditorScreenMode>
    {
        public ScreenSelectionTabControl()
        {
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;

            TabContainer.RelativeSizeAxes &= ~Axes.X;
            TabContainer.AutoSizeAxes = Axes.X;
            TabContainer.Padding = new MarginPadding();

            AddInternal(new Box
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                RelativeSizeAxes = Axes.X,
                Height = 1,
                Colour = Color4.White.Opacity(0.2f),
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AccentColour = colours.Yellow;
        }

        protected override Dropdown<EditorScreenMode> CreateDropdown() => null;

        protected override TabItem<EditorScreenMode> CreateTabItem(EditorScreenMode value) => new TabItem(value);

        private class TabItem : OsuTabItem
        {
            private const float transition_length = 250;

            public TabItem(EditorScreenMode value)
                : base(value)
            {
                Text.Margin = new MarginPadding();
                Text.Anchor = Anchor.CentreLeft;
                Text.Origin = Anchor.CentreLeft;
            }

            protected override void OnActivated()
            {
                base.OnActivated();
                Bar.ScaleTo(new Vector2(1, 5), transition_length, Easing.OutQuint);
            }

            protected override void OnDeactivated()
            {
                base.OnDeactivated();
                Bar.ScaleTo(Vector2.One, transition_length, Easing.OutQuint);
            }
        }
    }