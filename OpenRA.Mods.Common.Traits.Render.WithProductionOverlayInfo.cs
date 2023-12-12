	[Desc("Renders an animation when the Production trait of the actor is activated.",
		"Works both with per player ClassicProductionQueue and per building ProductionQueue, but needs any of these.")]
	public class WithProductionOverlayInfo : PausableConditionalTraitInfo, Requires<RenderSpritesInfo>, Requires<BodyOrientationInfo>, Requires<ProductionInfo>
	{
		[Desc("Queues that should be producing for this overlay to render.")]
		public readonly HashSet<string> Queues = new HashSet<string>();

		[SequenceReference]
		[Desc("Sequence name to use")]
		public readonly string Sequence = "production-overlay";

		[Desc("Position relative to body")]
		public readonly WVec Offset = WVec.Zero;

		[PaletteReference(nameof(IsPlayerPalette))]
		[Desc("Custom palette name")]
		public readonly string Palette = null;

		[Desc("Custom palette is a player palette BaseName")]
		public readonly bool IsPlayerPalette = false;

		public override object Create(ActorInitializer init) { return new WithProductionOverlay(init.Self, this); }
	}