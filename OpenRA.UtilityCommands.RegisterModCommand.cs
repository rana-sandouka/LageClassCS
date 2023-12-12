	class RegisterModCommand : IUtilityCommand
	{
		string IUtilityCommand.Name { get { return "--register-mod"; } }
		bool IUtilityCommand.ValidateArguments(string[] args)
		{
			return args.Length >= 3 && new string[] { "system", "user", "both" }.Contains(args[2]);
		}

		[Desc("LAUNCHPATH (system|user|both)", "Generates a mod metadata entry for the in-game mod switcher.")]
		void IUtilityCommand.Run(Utility utility, string[] args)
		{
			ModRegistration type = 0;
			if (args[2] == "system" || args[2] == "both")
				type |= ModRegistration.System;

			if (args[2] == "user" || args[2] == "both")
				type |= ModRegistration.User;

			new ExternalMods().Register(utility.ModData.Manifest, args[1], Enumerable.Empty<string>(), type);
		}
	}