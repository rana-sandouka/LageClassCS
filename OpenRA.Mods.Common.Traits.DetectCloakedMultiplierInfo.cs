	[Desc("Modifies the cloak detection range of this actor.")]
	public class DetectCloakedMultiplierInfo : ConditionalTraitInfo
	{
		[FieldLoader.Require]
		[Desc("Percentage modifier to apply.")]
		public readonly int Modifier = 100;

		public override object Create(ActorInitializer init) { return new DetectCloakedMultiplier(this); }
	}