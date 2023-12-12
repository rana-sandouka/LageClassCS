	public class WithHarvestAnimationInfo : TraitInfo, Requires<WithSpriteBodyInfo>, Requires<HarvesterInfo>
	{
		[SequenceReference]
		[Desc("Displayed while harvesting.")]
		public readonly string HarvestSequence = "harvest";

		[Desc("Which sprite body to play the animation on.")]
		public readonly string Body = "body";

		public override object Create(ActorInitializer init) { return new WithHarvestAnimation(init, this); }
	}