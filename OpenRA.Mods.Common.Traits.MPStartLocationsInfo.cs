	[Desc("Allows the map to have working spawnpoints. Also controls the 'Separate Team Spawns' checkbox in the lobby options.")]
	public class MPStartLocationsInfo : TraitInfo, ILobbyOptions, IAssignSpawnPointsInfo
	{
		public readonly WDist InitialExploreRange = WDist.FromCells(5);

		[Translate]
		[Desc("Descriptive label for the spawn positions checkbox in the lobby.")]
		public readonly string SeparateTeamSpawnsCheckboxLabel = "Separate Team Spawns";

		[Translate]
		[Desc("Tooltip description for the spawn positions checkbox in the lobby.")]
		public readonly string SeparateTeamSpawnsCheckboxDescription = "Players without assigned spawn points will start as far as possible from enemy players";

		[Desc("Default value of the spawn positions checkbox in the lobby.")]
		public readonly bool SeparateTeamSpawnsCheckboxEnabled = true;

		[Desc("Prevent the spawn positions state from being changed in the lobby.")]
		public readonly bool SeparateTeamSpawnsCheckboxLocked = false;

		[Desc("Whether to display the spawn positions checkbox in the lobby.")]
		public readonly bool SeparateTeamSpawnsCheckboxVisible = true;

		[Desc("Display order for the spawn positions checkbox in the lobby.")]
		public readonly int SeparateTeamSpawnsCheckboxDisplayOrder = 0;

		public override object Create(ActorInitializer init) { return new MPStartLocations(this); }

		IEnumerable<LobbyOption> ILobbyOptions.LobbyOptions(Ruleset rules)
		{
			yield return new LobbyBooleanOption(
				"separateteamspawns",
				SeparateTeamSpawnsCheckboxLabel,
				SeparateTeamSpawnsCheckboxDescription,
				SeparateTeamSpawnsCheckboxVisible,
				SeparateTeamSpawnsCheckboxDisplayOrder,
				SeparateTeamSpawnsCheckboxEnabled,
				SeparateTeamSpawnsCheckboxLocked);
		}

		class AssignSpawnLocationsState
		{
			public CPos[] SpawnLocations;
			public List<int> AvailableSpawnPoints;
			public readonly Dictionary<int, Session.Client> OccupiedSpawnPoints = new Dictionary<int, Session.Client>();
		}

		object IAssignSpawnPointsInfo.InitializeState(MapPreview map, Session lobbyInfo)
		{
			var state = new AssignSpawnLocationsState();

			// Initialize the list of unoccupied spawn points for AssignSpawnLocations to pick from
			state.SpawnLocations = map.SpawnPoints;
			state.AvailableSpawnPoints = LobbyUtils.AvailableSpawnPoints(map.SpawnPoints.Length, lobbyInfo);
			foreach (var kv in lobbyInfo.Slots)
			{
				var client = lobbyInfo.ClientInSlot(kv.Key);
				if (client == null || client.SpawnPoint == 0)
					continue;

				state.AvailableSpawnPoints.Remove(client.SpawnPoint);
				state.OccupiedSpawnPoints.Add(client.SpawnPoint, client);
			}

			return state;
		}

		int IAssignSpawnPointsInfo.AssignSpawnPoint(object stateObject, Session lobbyInfo, Session.Client client, MersenneTwister playerRandom)
		{
			var state = (AssignSpawnLocationsState)stateObject;
			var separateTeamSpawns = lobbyInfo.GlobalSettings.OptionOrDefault("separateteamspawns", SeparateTeamSpawnsCheckboxEnabled);

			if (client.SpawnPoint > 0 && client.SpawnPoint <= state.SpawnLocations.Length)
				return client.SpawnPoint;

			var spawnPoint = state.OccupiedSpawnPoints.Count == 0 || !separateTeamSpawns
				? state.AvailableSpawnPoints.Random(playerRandom)
				: state.AvailableSpawnPoints // pick the most distant spawnpoint from everyone else
					.Select(s => (Cell: state.SpawnLocations[s - 1], Index: s))
					.MaxBy(s => state.OccupiedSpawnPoints.Sum(kv => (state.SpawnLocations[kv.Key - 1] - s.Cell).LengthSquared)).Index;

			state.AvailableSpawnPoints.Remove(spawnPoint);
			state.OccupiedSpawnPoints.Add(spawnPoint, client);
			return spawnPoint;
		}
	}