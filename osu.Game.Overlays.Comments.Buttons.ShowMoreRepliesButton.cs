    public class ShowMoreRepliesButton : LoadingButton
    {
        protected override IEnumerable<Drawable> EffectTargets => new[] { text };

        private OsuSpriteText text;

        public ShowMoreRepliesButton()
        {
            AutoSizeAxes = Axes.Both;
            LoadingAnimationSize = new Vector2(8);
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            IdleColour = colourProvider.Light2;
            HoverColour = colourProvider.Light1;
        }

        protected override Drawable CreateContent() => new Container
        {
            AutoSizeAxes = Axes.Both,
            Child = text = new OsuSpriteText
            {
                AlwaysPresent = true,
                Font = OsuFont.GetFont(size: 12, weight: FontWeight.SemiBold),
                Text = "show more"
            }
        };

        protected override void OnLoadStarted() => text.FadeOut(200, Easing.OutQuint);

        protected override void OnLoadFinished() => text.FadeIn(200, Easing.OutQuint);
    }