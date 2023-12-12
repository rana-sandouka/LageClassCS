    public class DateTooltip : VisibilityContainer, ITooltip
    {
        private readonly OsuSpriteText dateText, timeText;
        private readonly Box background;

        public DateTooltip()
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;
            CornerRadius = 5;

            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        dateText = new OsuSpriteText
                        {
                            Font = OsuFont.GetFont(size: 12, weight: FontWeight.Bold),
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                        },
                        timeText = new OsuSpriteText
                        {
                            Font = OsuFont.GetFont(size: 12, weight: FontWeight.Regular),
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                        }
                    }
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            background.Colour = colours.GreySeafoamDarker;
            timeText.Colour = colours.BlueLighter;
        }

        protected override void PopIn() => this.FadeIn(200, Easing.OutQuint);
        protected override void PopOut() => this.FadeOut(200, Easing.OutQuint);

        public bool SetContent(object content)
        {
            if (!(content is DateTimeOffset date))
                return false;

            dateText.Text = $"{date:d MMMM yyyy} ";
            timeText.Text = $"{date:HH:mm:ss \"UTC\"z}";
            return true;
        }

        public void Move(Vector2 pos) => Position = pos;
    }