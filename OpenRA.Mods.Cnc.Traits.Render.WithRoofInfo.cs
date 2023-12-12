	[Desc("Provides an overlay for the Tiberian Dawn hover craft.")]
	public class WithRoofInfo : TraitInfo, Requires<RenderSpritesInfo>
	{
		[SequenceReference]
		public readonly string Sequence = "roof";

		public override object Create(ActorInitializer init) { return new WithRoof(init.Self, this); }
	}