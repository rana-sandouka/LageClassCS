	[Desc("Renders target lines between order waypoints.")]
	public class DrawLineToTargetInfo : TraitInfo
	{
		[Desc("Delay (in milliseconds) before the target lines disappear.")]
		public readonly int Delay = 2400;

		[Desc("Width (in pixels) of the target lines.")]
		public readonly int LineWidth = 1;

		[Desc("Width (in pixels) of the queued target lines.")]
		public readonly int QueuedLineWidth = 1;

		[Desc("Width (in pixels) of the end node markers.")]
		public readonly int MarkerWidth = 2;

		[Desc("Width (in pixels) of the queued end node markers.")]
		public readonly int QueuedMarkerWidth = 2;

		public override object Create(ActorInitializer init) { return new DrawLineToTarget(init.Self, this); }
	}