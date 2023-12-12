    public class BottomBarContainer : Container
    {
        private const float corner_radius = 5;
        private const float contents_padding = 15;

        protected readonly IBindable<WorkingBeatmap> Beatmap = new Bindable<WorkingBeatmap>();

        protected readonly IBindable<Track> Track = new Bindable<Track>();

        private readonly Drawable background;
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        public BottomBarContainer()
        {
            Masking = true;
            CornerRadius = corner_radius;

            InternalChildren = new[]
            {
                background = new Box { RelativeSizeAxes = Axes.Both },
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = contents_padding },
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(IBindable<WorkingBeatmap> beatmap, OsuColour colours, EditorClock clock)
        {
            Beatmap.BindTo(beatmap);
            Track.BindTo(clock.Track);

            background.Colour = colours.Gray1;
        }
    }