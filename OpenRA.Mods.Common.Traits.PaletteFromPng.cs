	class PaletteFromPng : ILoadsPalettes, IProvidesAssetBrowserPalettes
	{
		readonly World world;
		readonly PaletteFromPngInfo info;
		public PaletteFromPng(World world, PaletteFromPngInfo info)
		{
			this.world = world;
			this.info = info;
		}

		public void LoadPalettes(WorldRenderer wr)
		{
			if (info.Tileset != null && info.Tileset.ToLowerInvariant() != world.Map.Tileset.ToLowerInvariant())
				return;

			wr.AddPalette(info.Name, ((IProvidesCursorPaletteInfo)info).ReadPalette(world.Map), info.AllowModifiers);
		}

		public IEnumerable<string> PaletteNames
		{
			get
			{
				// Only expose the palette if it is available for the shellmap's tileset (which is a requirement for its use).
				if (info.Tileset == null || info.Tileset == world.Map.Rules.TileSet.Id)
					yield return info.Name;
			}
		}
	}