    public abstract class LoadingButton : OsuHoverContainer
    {
        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;

                Enabled.Value = !isLoading;

                if (value)
                {
                    loading.Show();
                    OnLoadStarted();
                }
                else
                {
                    loading.Hide();
                    OnLoadFinished();
                }
            }
        }

        public Vector2 LoadingAnimationSize
        {
            get => loading.Size;
            set => loading.Size = value;
        }

        private readonly LoadingSpinner loading;

        protected LoadingButton()
        {
            AddRange(new[]
            {
                CreateContent(),
                loading = new LoadingSpinner
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(12)
                }
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (!Enabled.Value)
                return false;

            try
            {
                return base.OnClick(e);
            }
            finally
            {
                // run afterwards as this will disable this button.
                IsLoading = true;
            }
        }

        protected virtual void OnLoadStarted()
        {
        }

        protected virtual void OnLoadFinished()
        {
        }

        protected abstract Drawable CreateContent();
    }