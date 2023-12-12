	[Desc("Replace with another actor when a resource spawns adjacent.")]
	public class TransformsNearResourcesInfo : TraitInfo
	{
		[FieldLoader.Require]
		[ActorReference]
		public readonly string IntoActor = null;

		public readonly CVec Offset = CVec.Zero;

		[Desc("Don't render the make animation.")]
		public readonly bool SkipMakeAnims = false;

		[FieldLoader.Require]
		[Desc("Resource type which triggers the transformation.")]
		public readonly string Type = null;

		[Desc("Resource density threshold which is required.")]
		public readonly int Density = 1;

		[Desc("This many adjacent resource tiles are required.")]
		public readonly int Adjacency = 1;

		[Desc("The range of time (in ticks) until the transformation starts.")]
		public readonly int[] Delay = { 1000, 3000 };

		public override object Create(ActorInitializer init) { return new TransformsNearResources(init.Self, this); }
	}