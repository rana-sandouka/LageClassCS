    public abstract class ScrollingPlayfield : Playfield
    {
        protected readonly IBindable<ScrollingDirection> Direction = new Bindable<ScrollingDirection>();

        public new ScrollingHitObjectContainer HitObjectContainer => (ScrollingHitObjectContainer)base.HitObjectContainer;

        [Resolved]
        protected IScrollingInfo ScrollingInfo { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Direction.BindTo(ScrollingInfo.Direction);
        }

        /// <summary>
        /// Given a position in screen space, return the time within this column.
        /// </summary>
        public virtual double TimeAtScreenSpacePosition(Vector2 screenSpacePosition) => HitObjectContainer.TimeAtScreenSpacePosition(screenSpacePosition);

        /// <summary>
        /// Given a time, return the screen space position within this column.
        /// </summary>
        public virtual Vector2 ScreenSpacePositionAtTime(double time) => HitObjectContainer.ScreenSpacePositionAtTime(time);

        protected sealed override HitObjectContainer CreateHitObjectContainer() => new ScrollingHitObjectContainer();
    }