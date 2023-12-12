	[ContentProcessor(DisplayName = "Aseprite Processor")]
	public class AsepriteProcessor : ContentProcessor<AsepriteFile, ProcessedAseprite> {
		public override ProcessedAseprite Process(AsepriteFile input, ContentProcessorContext context) {
			return new ProcessedAseprite {
				Aseprite = input,
				Log = context.Logger
			};
		}
	}