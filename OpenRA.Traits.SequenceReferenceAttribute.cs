	[AttributeUsage(AttributeTargets.Field)]
	public sealed class SequenceReferenceAttribute : Attribute
	{
		// The field name in the same trait info that contains the image name.
		public readonly string ImageReference;
		public readonly bool Prefix;
		public readonly bool AllowNullImage;
		public readonly LintDictionaryReference DictionaryReference;

		public SequenceReferenceAttribute(string imageReference = null, bool prefix = false, bool allowNullImage = false,
			LintDictionaryReference dictionaryReference = LintDictionaryReference.None)
		{
			ImageReference = imageReference;
			Prefix = prefix;
			AllowNullImage = allowNullImage;
			DictionaryReference = dictionaryReference;
		}
	}