	[Desc("Controls the 'Creeps' checkbox in the lobby options.")]
	public class MapCreepsInfo : TraitInfo, ILobbyOptions
	{
		[Translate]
		[Desc("Descriptive label for the creeps checkbox in the lobby.")]
		public readonly string CheckboxLabel = "Creep Actors";

		[Translate]
		[Desc("Tooltip description for the creeps checkbox in the lobby.")]
		public readonly string CheckboxDescription = "Hostile forces spawn on the battlefield";

		[Desc("Default value of the creeps checkbox in the lobby.")]
		public readonly bool CheckboxEnabled = true;

		[Desc("Prevent the creeps state from being changed in the lobby.")]
		public readonly bool CheckboxLocked = false;

		[Desc("Whether to display the creeps checkbox in the lobby.")]
		public readonly bool CheckboxVisible = true;

		[Desc("Display order for the creeps checkbox in the lobby.")]
		public readonly int CheckboxDisplayOrder = 0;

		IEnumerable<LobbyOption> ILobbyOptions.LobbyOptions(Ruleset rules)
		{
			yield return new LobbyBooleanOption("creeps", CheckboxLabel, CheckboxDescription, CheckboxVisible, CheckboxDisplayOrder, CheckboxEnabled, CheckboxLocked);
		}

		public override object Create(ActorInitializer init) { return new MapCreeps(this); }
	}