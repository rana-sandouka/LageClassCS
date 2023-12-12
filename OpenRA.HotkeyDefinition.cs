	public sealed class HotkeyDefinition
	{
		public readonly string Name;
		public readonly Hotkey Default = Hotkey.Invalid;
		public readonly string Description = "";
		public readonly HashSet<string> Types = new HashSet<string>();
		public bool HasDuplicates { get; internal set; }

		public HotkeyDefinition(string name, MiniYaml node)
		{
			Name = name;

			if (!string.IsNullOrEmpty(node.Value))
				Default = FieldLoader.GetValue<Hotkey>("value", node.Value);

			var descriptionNode = node.Nodes.FirstOrDefault(n => n.Key == "Description");
			if (descriptionNode != null)
				Description = descriptionNode.Value.Value;

			var typesNode = node.Nodes.FirstOrDefault(n => n.Key == "Types");
			if (typesNode != null)
				Types = FieldLoader.GetValue<HashSet<string>>("Types", typesNode.Value.Value);
		}
	}