    public class SpectatorResultsScreen : SoloResultsScreen
    {
        public SpectatorResultsScreen(ScoreInfo score)
            : base(score, false)
        {
        }

        [Resolved]
        private SpectatorStreamingClient spectatorStreaming { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            spectatorStreaming.OnUserBeganPlaying += userBeganPlaying;
        }

        private void userBeganPlaying(int userId, SpectatorState state)
        {
            if (userId == Score.UserID)
            {
                Schedule(() =>
                {
                    if (this.IsCurrentScreen()) this.Exit();
                });
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (spectatorStreaming != null)
                spectatorStreaming.OnUserBeganPlaying -= userBeganPlaying;
        }
    }