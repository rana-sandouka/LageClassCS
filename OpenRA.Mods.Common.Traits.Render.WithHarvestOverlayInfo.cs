	[Desc("Displays an overlay whenever resources are harvested by the actor.")]
	class WithHarvestOverlayInfo : TraitInfo, Requires<RenderSpritesInfo>, Requires<BodyOrientationInfo>
	{
		[SequenceReference]
		[Desc("Sequence name to use")]
		public readonly string Sequence = "harvest";

		[Desc("Position relative to body")]
		public readonly WVec LocalOffset = WVec.Zero;

		[PaletteReference]
		public readonly string Palette = "effect";

		public override object Create(ActorInitializer init) { return new WithHarvestOverlay(init.Self, this); }
	}