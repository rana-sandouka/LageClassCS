    public class PaginatedScoreContainer : PaginatedProfileSubsection<APILegacyScoreInfo>
    {
        private readonly ScoreType type;

        public PaginatedScoreContainer(ScoreType type, Bindable<User> user, string headerText, CounterVisibilityState counterVisibilityState, string missingText = "")
            : base(user, headerText, missingText, counterVisibilityState)
        {
            this.type = type;

            ItemsPerPage = 5;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ItemsContainer.Direction = FillDirection.Vertical;
        }

        protected override int GetCount(User user)
        {
            switch (type)
            {
                case ScoreType.Firsts:
                    return user.ScoresFirstCount;

                default:
                    return 0;
            }
        }

        protected override void OnItemsReceived(List<APILegacyScoreInfo> items)
        {
            if (VisiblePages == 0)
                drawableItemIndex = 0;

            base.OnItemsReceived(items);

            if (type == ScoreType.Recent)
                SetCount(items.Count);
        }

        protected override APIRequest<List<APILegacyScoreInfo>> CreateRequest() =>
            new GetUserScoresRequest(User.Value.Id, type, VisiblePages++, ItemsPerPage);

        private int drawableItemIndex;

        protected override Drawable CreateDrawableItem(APILegacyScoreInfo model)
        {
            switch (type)
            {
                default:
                    return new DrawableProfileScore(model.CreateScoreInfo(Rulesets));

                case ScoreType.Best:
                    return new DrawableProfileWeightedScore(model.CreateScoreInfo(Rulesets), Math.Pow(0.95, drawableItemIndex++));
            }
        }
    }