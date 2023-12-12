	[Desc("Creates a building placement preview based on the map editor actor preview.")]
	public class D2kActorPreviewPlaceBuildingPreviewInfo : ActorPreviewPlaceBuildingPreviewInfo
	{
		[Desc("Terrain types that should show the 'unsafe' footprint tile.")]
		public readonly HashSet<string> UnsafeTerrainTypes = new HashSet<string> { "Rock" };

		[Desc("Only check for 'unsafe' footprint tiles when you have these prerequisites.")]
		public readonly string[] RequiresPrerequisites = { };

		protected override IPlaceBuildingPreview CreatePreview(WorldRenderer wr, ActorInfo ai, TypeDictionary init)
		{
			return new D2kActorPreviewPlaceBuildingPreviewPreview(wr, ai, this, init);
		}

		public override object Create(ActorInitializer init)
		{
			return new D2kActorPreviewPlaceBuildingPreview();
		}
	}