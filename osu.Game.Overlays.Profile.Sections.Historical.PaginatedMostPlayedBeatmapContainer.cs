    public class PaginatedMostPlayedBeatmapContainer : PaginatedProfileSubsection<APIUserMostPlayedBeatmap>
    {
        public PaginatedMostPlayedBeatmapContainer(Bindable<User> user)
            : base(user, "Most Played Beatmaps", "No records. :(", CounterVisibilityState.AlwaysVisible)
        {
            ItemsPerPage = 5;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ItemsContainer.Direction = FillDirection.Vertical;
        }

        protected override int GetCount(User user) => user.BeatmapPlaycountsCount;

        protected override APIRequest<List<APIUserMostPlayedBeatmap>> CreateRequest() =>
            new GetUserMostPlayedBeatmapsRequest(User.Value.Id, VisiblePages++, ItemsPerPage);

        protected override Drawable CreateDrawableItem(APIUserMostPlayedBeatmap model) =>
            new DrawableMostPlayedBeatmap(model.GetBeatmapInfo(Rulesets), model.PlayCount);
    }