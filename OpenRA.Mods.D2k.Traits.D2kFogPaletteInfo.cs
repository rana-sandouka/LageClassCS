	class D2kFogPaletteInfo : TraitInfo
	{
		[PaletteDefinition]
		[FieldLoader.Require]
		[Desc("Internal palette name")]
		public readonly string Name = null;

		[PaletteReference]
		[FieldLoader.Require]
		[Desc("The name of the shroud palette to base off.")]
		public readonly string BasePalette = null;

		[Desc("Allow palette modifiers to change the palette.")]
		public readonly bool AllowModifiers = true;

		public override object Create(ActorInitializer init) { return new D2kFogPalette(this); }
	}