	public class TerrainTemplateInfo
	{
		public readonly ushort Id;
		public readonly string[] Images;
		public readonly int[] Frames;
		public readonly int2 Size;
		public readonly bool PickAny;
		public readonly string[] Categories;
		public readonly string Palette;

		readonly TerrainTileInfo[] tileInfo;

		public TerrainTemplateInfo(TileSet tileSet, MiniYaml my)
		{
			FieldLoader.Load(this, my);

			var nodes = my.ToDictionary()["Tiles"].Nodes;

			if (!PickAny)
			{
				tileInfo = new TerrainTileInfo[Size.X * Size.Y];
				foreach (var node in nodes)
				{
					if (!int.TryParse(node.Key, out var key))
						throw new YamlException("Tileset `{0}` template `{1}` defines a frame `{2}` that is not a valid integer.".F(tileSet.Id, Id, node.Key));

					if (key < 0 || key >= tileInfo.Length)
						throw new YamlException("Tileset `{0}` template `{1}` references frame {2}, but only [0..{3}] are valid for a {4}x{5} Size template.".F(tileSet.Id, Id, key, tileInfo.Length - 1, Size.X, Size.Y));

					tileInfo[key] = LoadTileInfo(tileSet, node.Value);
				}
			}
			else
			{
				tileInfo = new TerrainTileInfo[nodes.Count];

				var i = 0;
				foreach (var node in nodes)
				{
					if (!int.TryParse(node.Key, out var key))
						throw new YamlException("Tileset `{0}` template `{1}` defines a frame `{2}` that is not a valid integer.".F(tileSet.Id, Id, node.Key));

					if (key != i++)
						throw new YamlException("Tileset `{0}` template `{1}` is missing a definition for frame {2}.".F(tileSet.Id, Id, i - 1));

					tileInfo[key] = LoadTileInfo(tileSet, node.Value);
				}
			}
		}

		static TerrainTileInfo LoadTileInfo(TileSet tileSet, MiniYaml my)
		{
			var tile = new TerrainTileInfo();
			FieldLoader.Load(tile, my);

			// Terrain type must be converted from a string to an index
			tile.GetType().GetField("TerrainType").SetValue(tile, tileSet.GetTerrainIndex(my.Value));

			// Fall back to the terrain-type color if necessary
			var overrideColor = tileSet.TerrainInfo[tile.TerrainType].Color;
			if (tile.MinColor == default(Color))
				tile.GetType().GetField("MinColor").SetValue(tile, overrideColor);

			if (tile.MaxColor == default(Color))
				tile.GetType().GetField("MaxColor").SetValue(tile, overrideColor);

			return tile;
		}

		public TerrainTileInfo this[int index] { get { return tileInfo[index]; } }

		public bool Contains(int index)
		{
			return index >= 0 && index < tileInfo.Length;
		}

		public int TilesCount
		{
			get { return tileInfo.Length; }
		}
	}