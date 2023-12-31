    public abstract class EditorScreenWithTimeline : EditorScreen
    {
        private const float vertical_margins = 10;
        private const float horizontal_margins = 20;

        private const float timeline_height = 110;

        private readonly BindableBeatDivisor beatDivisor = new BindableBeatDivisor();

        private Container timelineContainer;

        protected EditorScreenWithTimeline(EditorScreenMode type)
            : base(type)
        {
        }

        private Container mainContent;

        private LoadingSpinner spinner;

        [BackgroundDependencyLoader(true)]
        private void load([CanBeNull] BindableBeatDivisor beatDivisor)
        {
            if (beatDivisor != null)
                this.beatDivisor.BindTo(beatDivisor);

            Children = new Drawable[]
            {
                mainContent = new Container
                {
                    Name = "Main content",
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding
                    {
                        Horizontal = horizontal_margins,
                        Top = vertical_margins + timeline_height,
                        Bottom = vertical_margins
                    },
                    Child = spinner = new LoadingSpinner(true)
                    {
                        State = { Value = Visibility.Visible },
                    },
                },
                new Container
                {
                    Name = "Timeline",
                    RelativeSizeAxes = Axes.X,
                    Height = timeline_height,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black.Opacity(0.5f)
                        },
                        new Container
                        {
                            Name = "Timeline content",
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding { Horizontal = horizontal_margins, Vertical = vertical_margins },
                            Child = new GridContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                Content = new[]
                                {
                                    new Drawable[]
                                    {
                                        timelineContainer = new Container
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Padding = new MarginPadding { Right = 5 },
                                        },
                                        new BeatDivisorControl(beatDivisor) { RelativeSizeAxes = Axes.Both }
                                    },
                                },
                                ColumnDimensions = new[]
                                {
                                    new Dimension(),
                                    new Dimension(GridSizeMode.Absolute, 90),
                                }
                            },
                        }
                    }
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            LoadComponentAsync(CreateMainContent(), content =>
            {
                spinner.State.Value = Visibility.Hidden;

                mainContent.Add(content);
                content.FadeInFromZero(300, Easing.OutQuint);

                LoadComponentAsync(new TimelineArea
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        CreateTimelineContent(),
                    }
                }, t =>
                {
                    timelineContainer.Add(t);
                    OnTimelineLoaded(t);
                });
            });
        }

        protected virtual void OnTimelineLoaded(TimelineArea timelineArea)
        {
        }

        protected abstract Drawable CreateMainContent();

        protected virtual Drawable CreateTimelineContent() => new Container();
    }