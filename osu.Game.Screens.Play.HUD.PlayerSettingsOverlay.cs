    public class PlayerSettingsOverlay : VisibilityContainer
    {
        private const int fade_duration = 200;

        public bool ReplayLoaded;

        public readonly PlaybackSettings PlaybackSettings;

        public readonly VisualSettings VisualSettings;

        public PlayerSettingsOverlay()
        {
            AlwaysPresent = true;

            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            AutoSizeAxes = Axes.Both;

            Child = new FillFlowContainer<PlayerSettingsGroup>
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 20),
                Children = new PlayerSettingsGroup[]
                {
                    //CollectionSettings = new CollectionSettings(),
                    //DiscussionSettings = new DiscussionSettings(),
                    PlaybackSettings = new PlaybackSettings(),
                    VisualSettings = new VisualSettings { Expanded = false }
                }
            };
        }

        protected override void PopIn() => this.FadeIn(fade_duration);
        protected override void PopOut() => this.FadeOut(fade_duration);

        // We want to handle keyboard inputs all the time in order to trigger ToggleVisibility() when not visible
        public override bool PropagateNonPositionalInputSubTree => true;

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Repeat) return false;

            if (e.ControlPressed)
            {
                if (e.Key == Key.H && ReplayLoaded)
                {
                    ToggleVisibility();
                    return true;
                }
            }

            return base.OnKeyDown(e);
        }
    }