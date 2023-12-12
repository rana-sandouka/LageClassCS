	public class WithDockingAnimationInfo : TraitInfo<WithDockingAnimation>, Requires<WithSpriteBodyInfo>, Requires<HarvesterInfo>
	{
		[SequenceReference]
		[Desc("Displayed when docking to refinery.")]
		public readonly string DockSequence = "dock";

		[SequenceReference]
		[Desc("Looped while unloading at refinery.")]
		public readonly string DockLoopSequence = "dock-loop";
	}