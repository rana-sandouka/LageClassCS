		public class LobbyOptionState
		{
			public string Value;
			public string PreferredValue;

			public bool IsLocked;
			public bool IsEnabled { get { return Value == "True"; } }
		}