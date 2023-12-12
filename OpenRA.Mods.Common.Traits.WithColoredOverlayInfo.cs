	[Desc("Display a colored overlay when a timed condition is active.")]
	public class WithColoredOverlayInfo : ConditionalTraitInfo
	{
		[PaletteReference]
		[Desc("Palette to use when rendering the overlay")]
		public readonly string Palette = "invuln";

		public override object Create(ActorInitializer init) { return new WithColoredOverlay(this); }
	}