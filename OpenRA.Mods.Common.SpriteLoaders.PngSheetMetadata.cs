	public class PngSheetMetadata
	{
		public readonly ReadOnlyDictionary<string, string> Metadata;

		public PngSheetMetadata(Dictionary<string, string> metadata)
		{
			Metadata = new ReadOnlyDictionary<string, string>(metadata);
		}
	}