	[Desc("Modifies the bale values delivered to this refinery.")]
	public class RefineryResourceMultiplierInfo : ConditionalTraitInfo
	{
		[FieldLoader.Require]
		[Desc("Percentage modifier to apply.")]
		public readonly int Modifier = 100;

		public override object Create(ActorInitializer init) { return new RefineryResourceMultiplier(this); }
	}