    public abstract class OnlineViewContainer : Container
    {
        protected LoadingSpinner LoadingSpinner { get; private set; }

        protected override Container<Drawable> Content { get; } = new Container { RelativeSizeAxes = Axes.Both };

        private readonly string placeholderMessage;

        private Placeholder placeholder;

        private const double transform_duration = 300;

        [Resolved]
        protected IAPIProvider API { get; private set; }

        protected OnlineViewContainer(string placeholderMessage)
        {
            this.placeholderMessage = placeholderMessage;
        }

        private readonly IBindable<APIState> apiState = new Bindable<APIState>();

        [BackgroundDependencyLoader]
        private void load(IAPIProvider api)
        {
            InternalChildren = new Drawable[]
            {
                Content,
                placeholder = new LoginPlaceholder(placeholderMessage),
                LoadingSpinner = new LoadingSpinner
                {
                    Alpha = 0,
                }
            };

            apiState.BindTo(api.State);
            apiState.BindValueChanged(onlineStateChanged, true);
        }

        private void onlineStateChanged(ValueChangedEvent<APIState> state) => Schedule(() =>
        {
            switch (state.NewValue)
            {
                case APIState.Offline:
                    PopContentOut(Content);
                    placeholder.ScaleTo(0.8f).Then().ScaleTo(1, 3 * transform_duration, Easing.OutQuint);
                    placeholder.FadeInFromZero(2 * transform_duration, Easing.OutQuint);
                    LoadingSpinner.Hide();
                    break;

                case APIState.Online:
                    PopContentIn(Content);
                    placeholder.FadeOut(transform_duration / 2, Easing.OutQuint);
                    LoadingSpinner.Hide();
                    break;

                case APIState.Failing:
                case APIState.Connecting:
                    PopContentOut(Content);
                    LoadingSpinner.Show();
                    placeholder.FadeOut(transform_duration / 2, Easing.OutQuint);
                    break;
            }
        });

        /// <summary>
        /// Applies a transform to the online content to make it hidden.
        /// </summary>
        protected virtual void PopContentOut(Drawable content) => content.FadeOut(transform_duration / 2, Easing.OutQuint);

        /// <summary>
        /// Applies a transform to the online content to make it visible.
        /// </summary>
        protected virtual void PopContentIn(Drawable content) => content.FadeIn(transform_duration, Easing.OutQuint);
    }