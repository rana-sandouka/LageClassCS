    public class PauseOverlay : GameplayMenuOverlay
    {
        public Action OnResume;

        public override bool IsPresent => base.IsPresent || pauseLoop.IsPlaying;

        public override string Header => "paused";
        public override string Description => "you're not going to do what i think you're going to do, are ya?";

        private SkinnableSound pauseLoop;

        protected override Action BackAction => () => InternalButtons.Children.First().Click();

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AddButton("Continue", colours.Green, () => OnResume?.Invoke());
            AddButton("Retry", colours.YellowDark, () => OnRetry?.Invoke());
            AddButton("Quit", new Color4(170, 27, 39, 255), () => OnQuit?.Invoke());

            AddInternal(pauseLoop = new SkinnableSound(new SampleInfo("Gameplay/pause-loop"))
            {
                Looping = true,
                Volume = { Value = 0 }
            });
        }

        protected override void PopIn()
        {
            base.PopIn();

            pauseLoop.VolumeTo(1.0f, TRANSITION_DURATION, Easing.InQuint);
            pauseLoop.Play();
        }

        protected override void PopOut()
        {
            base.PopOut();

            pauseLoop.VolumeTo(0, TRANSITION_DURATION, Easing.OutQuad).Finally(_ => pauseLoop.Stop());
        }
    }