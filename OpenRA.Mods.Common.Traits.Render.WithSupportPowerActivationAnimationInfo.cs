	[Desc("Replaces the building animation when a support power is triggered.")]
	public class WithSupportPowerActivationAnimationInfo : ConditionalTraitInfo, Requires<WithSpriteBodyInfo>
	{
		[SequenceReference]
		[Desc("Sequence name to use")]
		public readonly string Sequence = "active";

		[Desc("Which sprite body to play the animation on.")]
		public readonly string Body = "body";

		public override object Create(ActorInitializer init) { return new WithSupportPowerActivationAnimation(init.Self, this); }
	}