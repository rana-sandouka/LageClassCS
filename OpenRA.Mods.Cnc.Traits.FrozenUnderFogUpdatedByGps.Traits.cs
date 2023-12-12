		class Traits
		{
			public readonly FrozenActorLayer FrozenActorLayer;
			public readonly GpsWatcher GpsWatcher;
			public Traits(Player player, FrozenUnderFogUpdatedByGps frozenUnderFogUpdatedByGps)
			{
				FrozenActorLayer = player.FrozenActorLayer;
				GpsWatcher = player.PlayerActor.TraitOrDefault<GpsWatcher>();
				GpsWatcher.RegisterForOnGpsRefreshed(frozenUnderFogUpdatedByGps.self, frozenUnderFogUpdatedByGps);
			}
		}