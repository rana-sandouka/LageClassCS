        public class ScrollToTopButton : OsuHoverContainer
        {
            private const int fade_duration = 500;

            private Visibility state;

            public Visibility State
            {
                get => state;
                set
                {
                    if (value == state)
                        return;

                    state = value;
                    Enabled.Value = state == Visibility.Visible;
                    this.FadeTo(state == Visibility.Visible ? 1 : 0, fade_duration, Easing.OutQuint);
                }
            }

            protected override IEnumerable<Drawable> EffectTargets => new[] { background };

            private Color4 flashColour;

            private readonly Container content;
            private readonly Box background;

            public ScrollToTopButton()
            {
                Size = new Vector2(50);
                Alpha = 0;
                Add(content = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Shadow,
                        Offset = new Vector2(0f, 1f),
                        Radius = 3f,
                        Colour = Color4.Black.Opacity(0.25f),
                    },
                    Children = new Drawable[]
                    {
                        background = new Box
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(15),
                            Icon = FontAwesome.Solid.ChevronUp
                        }
                    }
                });

                TooltipText = "Scroll to top";
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                IdleColour = colourProvider.Background6;
                HoverColour = colourProvider.Background5;
                flashColour = colourProvider.Light1;
            }

            protected override bool OnClick(ClickEvent e)
            {
                background.FlashColour(flashColour, 800, Easing.OutQuint);
                return base.OnClick(e);
            }

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                content.ScaleTo(0.75f, 2000, Easing.OutQuint);
                return true;
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                content.ScaleTo(1, 1000, Easing.OutElastic);
                base.OnMouseUp(e);
            }
        }