	class D2kActorPreviewPlaceBuildingPreviewPreview : ActorPreviewPlaceBuildingPreviewPreview
	{
		readonly D2kActorPreviewPlaceBuildingPreviewInfo info;
		readonly bool checkUnsafeTiles;
		readonly Sprite buildOk;
		readonly Sprite buildUnsafe;
		readonly Sprite buildBlocked;
		readonly CachedTransform<CPos, List<CPos>> unpathableCells;

		public D2kActorPreviewPlaceBuildingPreviewPreview(WorldRenderer wr, ActorInfo ai, D2kActorPreviewPlaceBuildingPreviewInfo info, TypeDictionary init)
			: base(wr, ai, info, init)
		{
			this.info = info;

			var world = wr.World;
			var sequences = world.Map.Rules.Sequences;

			var techTree = init.Get<OwnerInit>().Value(world).PlayerActor.Trait<TechTree>();
			checkUnsafeTiles = info.RequiresPrerequisites.Any() && techTree.HasPrerequisites(info.RequiresPrerequisites);

			buildOk = sequences.GetSequence("overlay", "build-valid").GetSprite(0);
			buildUnsafe = sequences.GetSequence("overlay", "build-unsafe").GetSprite(0);
			buildBlocked = sequences.GetSequence("overlay", "build-invalid").GetSprite(0);

			var buildingInfo = ai.TraitInfo<BuildingInfo>();
			unpathableCells = new CachedTransform<CPos, List<CPos>>(topLeft => buildingInfo.OccupiedTiles(topLeft).ToList());
		}

		protected override IEnumerable<IRenderable> RenderFootprint(WorldRenderer wr, CPos topLeft, Dictionary<CPos, PlaceBuildingCellType> footprint,
			PlaceBuildingCellType filter = PlaceBuildingCellType.Invalid | PlaceBuildingCellType.Valid | PlaceBuildingCellType.LineBuild)
		{
			var cellPalette = wr.Palette(info.Palette);
			var linePalette = wr.Palette(info.LineBuildSegmentPalette);
			var topLeftPos = wr.World.Map.CenterOfCell(topLeft);

			var candidateSafeTiles = unpathableCells.Update(topLeft);
			foreach (var c in footprint)
			{
				if ((c.Value & filter) == 0)
					continue;

				var tile = HasFlag(c.Value, PlaceBuildingCellType.Invalid) ? buildBlocked :
					(checkUnsafeTiles && candidateSafeTiles.Contains(c.Key) && info.UnsafeTerrainTypes.Contains(wr.World.Map.GetTerrainInfo(c.Key).Type))
					? buildUnsafe : buildOk;

				var pal = HasFlag(c.Value, PlaceBuildingCellType.LineBuild) ? linePalette : cellPalette;
				var pos = wr.World.Map.CenterOfCell(c.Key);
				var offset = new WVec(0, 0, topLeftPos.Z - pos.Z);
				yield return new SpriteRenderable(tile, pos, offset, -511, pal, 1f, true, true);
			}
		}
	}