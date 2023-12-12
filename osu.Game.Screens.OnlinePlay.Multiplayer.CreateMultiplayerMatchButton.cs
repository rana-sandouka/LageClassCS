    public class CreateMultiplayerMatchButton : PurpleTriangleButton
    {
        private IBindable<bool> isConnected;
        private IBindable<bool> operationInProgress;

        [Resolved]
        private StatefulMultiplayerClient multiplayerClient { get; set; }

        [Resolved]
        private OngoingOperationTracker ongoingOperationTracker { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Triangles.TriangleScale = 1.5f;

            Text = "Create room";

            isConnected = multiplayerClient.IsConnected.GetBoundCopy();
            operationInProgress = ongoingOperationTracker.InProgress.GetBoundCopy();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            isConnected.BindValueChanged(_ => Scheduler.AddOnce(updateState));
            operationInProgress.BindValueChanged(_ => Scheduler.AddOnce(updateState), true);
        }

        private void updateState() => Enabled.Value = isConnected.Value && !operationInProgress.Value;
    }