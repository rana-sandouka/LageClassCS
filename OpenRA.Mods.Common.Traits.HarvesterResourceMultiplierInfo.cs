	[Desc("Modifies the bale values of this harvester.")]
	public class HarvesterResourceMultiplierInfo : ConditionalTraitInfo
	{
		[FieldLoader.Require]
		[Desc("Percentage modifier to apply.")]
		public readonly int Modifier = 100;

		public override object Create(ActorInitializer init) { return new HarvesterResourceMultiplier(this); }
	}