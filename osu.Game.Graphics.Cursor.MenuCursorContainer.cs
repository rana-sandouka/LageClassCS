    public class MenuCursorContainer : Container, IProvideCursor
    {
        protected override Container<Drawable> Content => content;
        private readonly Container content;

        /// <summary>
        /// Whether any cursors can be displayed.
        /// </summary>
        internal bool CanShowCursor = true;

        public CursorContainer Cursor { get; }
        public bool ProvidingUserCursor => true;

        public MenuCursorContainer()
        {
            AddRangeInternal(new Drawable[]
            {
                Cursor = new MenuCursor { State = { Value = Visibility.Hidden } },
                content = new Container { RelativeSizeAxes = Axes.Both }
            });
        }

        private InputManager inputManager;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            inputManager = GetContainingInputManager();
        }

        private IProvideCursor currentTarget;

        protected override void Update()
        {
            base.Update();

            var lastMouseSource = inputManager.CurrentState.Mouse.LastSource;
            bool hasValidInput = lastMouseSource != null && !(lastMouseSource is ISourcedFromTouch);

            if (!hasValidInput || !CanShowCursor)
            {
                currentTarget?.Cursor?.Hide();
                currentTarget = null;
                return;
            }

            IProvideCursor newTarget = this;

            foreach (var d in inputManager.HoveredDrawables)
            {
                if (d is IProvideCursor p && p.ProvidingUserCursor)
                {
                    newTarget = p;
                    break;
                }
            }

            if (currentTarget == newTarget)
                return;

            currentTarget?.Cursor?.Hide();
            newTarget.Cursor?.Show();

            currentTarget = newTarget;
        }
    }