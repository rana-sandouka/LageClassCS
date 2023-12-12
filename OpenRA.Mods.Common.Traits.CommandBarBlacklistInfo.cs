	[Desc("Blacklist certain order types to disable on the command bar when this unit is selected.")]
	public class CommandBarBlacklistInfo : TraitInfo<CommandBarBlacklist>
	{
		[Desc("Disable the 'Stop' button for this actor.")]
		public readonly bool DisableStop = true;

		[Desc("Disable the 'Waypoint Mode' button for this actor.")]
		public readonly bool DisableWaypointMode = true;
	}