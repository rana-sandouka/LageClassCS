	[Desc("Define a player palette by swapping palette indices.")]
	public class IndexedPlayerPaletteInfo : TraitInfo, IRulesetLoaded
	{
		[PaletteReference]
		[Desc("The name of the palette to base off.")]
		public readonly string BasePalette = null;

		[PaletteDefinition(true)]
		[Desc("The prefix for the resulting player palettes")]
		public readonly string BaseName = "player";

		[Desc("Remap these indices to player colors.")]
		public readonly int[] RemapIndex = { };

		[Desc("Allow palette modifiers to change the palette.")]
		public readonly bool AllowModifiers = true;

		public readonly Dictionary<string, int[]> PlayerIndex;

		public override object Create(ActorInitializer init) { return new IndexedPlayerPalette(this); }

		public void RulesetLoaded(Ruleset rules, ActorInfo ai)
		{
			foreach (var p in PlayerIndex)
				if (p.Value.Length != RemapIndex.Length)
					throw new YamlException("PlayerIndex for player `{0}` length does not match RemapIndex!".F(p.Key));
		}
	}