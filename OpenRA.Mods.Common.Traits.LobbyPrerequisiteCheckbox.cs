	public class LobbyPrerequisiteCheckbox : INotifyCreated, ITechTreePrerequisite
	{
		readonly LobbyPrerequisiteCheckboxInfo info;
		HashSet<string> prerequisites = new HashSet<string>();

		public LobbyPrerequisiteCheckbox(LobbyPrerequisiteCheckboxInfo info)
		{
			this.info = info;
		}

		void INotifyCreated.Created(Actor self)
		{
			var enabled = self.World.LobbyInfo.GlobalSettings.OptionOrDefault(info.ID, info.Enabled);
			if (enabled)
				prerequisites = info.Prerequisites;
		}

		IEnumerable<string> ITechTreePrerequisite.ProvidesPrerequisites { get { return prerequisites; } }
	}