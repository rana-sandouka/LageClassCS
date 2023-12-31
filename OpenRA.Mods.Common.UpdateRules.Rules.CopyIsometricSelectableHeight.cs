	public class CopyIsometricSelectableHeight : UpdateRule
	{
		public override string Name { get { return "Copy IsometricSelectable.Height from art*.ini definitions."; } }
		public override string Description
		{
			get
			{
				return "Reads building Height entries art.ini/artfs.ini/artmd.ini from the current working directory\n" +
					"and adds IsometricSelectable definitions to matching actors.";
			}
		}

		static readonly string[] SourceFiles = { "art.ini", "artfs.ini", "artmd.ini" };

		readonly Dictionary<string, int> selectionHeight = new Dictionary<string, int>();

		bool complete;

		public override IEnumerable<string> BeforeUpdate(ModData modData)
		{
			if (complete)
				yield break;

			var grid = Game.ModData.Manifest.Get<MapGrid>();
			foreach (var filename in SourceFiles)
			{
				if (!File.Exists(filename))
					continue;

				var file = new IniFile(File.Open(filename, FileMode.Open));
				foreach (var section in file.Sections)
				{
					if (!section.Contains("Height"))
						continue;

					selectionHeight[section.Name] = (int)(float.Parse(section.GetValue("Height", "1")) * grid.TileSize.Height);
				}
			}
		}

		public override IEnumerable<string> AfterUpdate(ModData modData)
		{
			// Rule only applies to the default ruleset - skip maps
			complete = true;
			yield break;
		}

		public override IEnumerable<string> UpdateActorNode(ModData modData, MiniYamlNode actorNode)
		{
			if (complete || actorNode.LastChildMatching("IsometricSelectable") != null)
				yield break;

			var height = 0;
			if (!selectionHeight.TryGetValue(actorNode.Key.ToLowerInvariant(), out height))
				yield break;

			// Don't redefine the default value
			if (height == 24)
				yield break;

			var selection = new MiniYamlNode("IsometricSelectable", "");
			selection.AddNode("Height", FieldSaver.FormatValue(height));

			actorNode.AddNode(selection);
		}
	}