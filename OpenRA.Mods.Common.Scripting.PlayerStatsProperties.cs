	[ScriptPropertyGroup("Player")]
	public class PlayerStatsProperties : ScriptPlayerProperties, Requires<PlayerStatisticsInfo>
	{
		readonly PlayerStatistics stats;

		public PlayerStatsProperties(ScriptContext context, Player player)
			: base(context, player)
		{
			stats = player.PlayerActor.Trait<PlayerStatistics>();
		}

		[Desc("The combined value of units killed by this player.")]
		public int KillsCost { get { return stats.KillsCost; } }

		[Desc("The combined value of all units lost by this player.")]
		public int DeathsCost { get { return stats.DeathsCost; } }

		[Desc("The total number of units killed by this player.")]
		public int UnitsKilled { get { return stats.UnitsKilled; } }

		[Desc("The total number of units lost by this player.")]
		public int UnitsLost { get { return stats.UnitsDead; } }

		[Desc("The total number of buildings killed by this player.")]
		public int BuildingsKilled { get { return stats.BuildingsKilled; } }

		[Desc("The total number of buildings lost by this player.")]
		public int BuildingsLost { get { return stats.BuildingsDead; } }
	}