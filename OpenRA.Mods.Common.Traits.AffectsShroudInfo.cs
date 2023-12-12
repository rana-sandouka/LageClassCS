	public abstract class AffectsShroudInfo : ConditionalTraitInfo
	{
		public readonly WDist MinRange = WDist.Zero;

		public readonly WDist Range = WDist.Zero;

		[Desc("If >= 0, prevent cells that are this much higher than the actor from being revealed.")]
		public readonly int MaxHeightDelta = -1;

		[Desc("If > 0, force visibility to be recalculated if the unit moves within a cell by more than this distance.")]
		public readonly WDist MoveRecalculationThreshold = new WDist(256);

		[Desc("Possible values are CenterPosition (measure range from the center) and ",
			"Footprint (measure range from the footprint)")]
		public readonly VisibilityType Type = VisibilityType.Footprint;
	}