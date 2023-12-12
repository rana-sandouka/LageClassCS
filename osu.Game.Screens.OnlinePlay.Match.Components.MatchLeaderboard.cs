    public class MatchLeaderboard : Leaderboard<MatchLeaderboardScope, APIUserScoreAggregate>
    {
        [Resolved(typeof(Room), nameof(Room.RoomID))]
        private Bindable<int?> roomId { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            roomId.BindValueChanged(id =>
            {
                if (id.NewValue == null)
                    return;

                Scores = null;
                UpdateScores();
            }, true);
        }

        protected override bool IsOnlineScope => true;

        protected override APIRequest FetchScores(Action<IEnumerable<APIUserScoreAggregate>> scoresCallback)
        {
            if (roomId.Value == null)
                return null;

            var req = new GetRoomLeaderboardRequest(roomId.Value ?? 0);

            req.Success += r =>
            {
                scoresCallback?.Invoke(r.Leaderboard);
                TopScore = r.UserScore;
            };

            return req;
        }

        protected override LeaderboardScore CreateDrawableScore(APIUserScoreAggregate model, int index) => new MatchLeaderboardScore(model, index);

        protected override LeaderboardScore CreateDrawableTopScore(APIUserScoreAggregate model) => new MatchLeaderboardScore(model, model.Position, false);
    }