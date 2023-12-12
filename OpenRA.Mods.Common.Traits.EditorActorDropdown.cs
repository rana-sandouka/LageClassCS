	public class EditorActorDropdown : EditorActorOption
	{
		public readonly Dictionary<string, string> Labels;
		public readonly Func<EditorActorPreview, string> GetValue;
		public readonly Action<EditorActorPreview, string> OnChange;

		public EditorActorDropdown(string name, int displayOrder,
			Dictionary<string, string> labels,
			Func<EditorActorPreview, string> getValue,
			Action<EditorActorPreview, string> onChange)
			: base(name, displayOrder)
		{
			Labels = labels;
			GetValue = getValue;
			OnChange = onChange;
		}
	}