    public class MatchChatDisplay : StandAloneChatDisplay
    {
        [Resolved(typeof(Room), nameof(Room.RoomID))]
        private Bindable<int?> roomId { get; set; }

        [Resolved(typeof(Room), nameof(Room.ChannelId))]
        private Bindable<int> channelId { get; set; }

        [Resolved(CanBeNull = true)]
        private ChannelManager channelManager { get; set; }

        public MatchChatDisplay()
            : base(true)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            channelId.BindValueChanged(_ => updateChannel(), true);
        }

        private void updateChannel()
        {
            if (roomId.Value == null || channelId.Value == 0)
                return;

            Channel.Value = channelManager?.JoinChannel(new Channel { Id = channelId.Value, Type = ChannelType.Multiplayer, Name = $"#lazermp_{roomId.Value}" });
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            channelManager?.LeaveChannel(Channel.Value);
        }
    }