		class Filter
		{
			public GameType Type;
			public DateType Date;
			public DurationType Duration;
			public WinState Outcome;
			public string PlayerName;
			public string MapName;
			public string Faction;

			public bool IsEmpty
			{
				get
				{
					return Type == default(GameType)
						&& Date == default(DateType)
						&& Duration == default(DurationType)
						&& Outcome == default(WinState)
						&& string.IsNullOrEmpty(PlayerName)
						&& string.IsNullOrEmpty(MapName)
						&& string.IsNullOrEmpty(Faction);
				}
			}
		}