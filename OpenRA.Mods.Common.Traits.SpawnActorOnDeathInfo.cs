	[Desc("Spawn another actor immediately upon death.")]
	public class SpawnActorOnDeathInfo : ConditionalTraitInfo
	{
		[ActorReference]
		[FieldLoader.Require]
		[Desc("Actor to spawn on death.")]
		public readonly string Actor = null;

		[Desc("Probability the actor spawns.")]
		public readonly int Probability = 100;

		[Desc("Owner of the spawned actor. Allowed keywords:" +
			"'Victim', 'Killer' and 'InternalName'. " +
			"Falls back to 'InternalName' if 'Victim' is used " +
			"and the victim is defeated (see 'SpawnAfterDefeat').")]
		public readonly OwnerType OwnerType = OwnerType.Victim;

		[Desc("Map player to use when 'InternalName' is defined on 'OwnerType'.")]
		public readonly string InternalOwner = "Neutral";

		[Desc("Changes the effective (displayed) owner of the spawned actor to the old owner (victim).")]
		public readonly bool EffectiveOwnerFromOwner = false;

		[Desc("DeathType that triggers the actor spawn. " +
			"Leave empty to spawn an actor ignoring the DeathTypes.")]
		public readonly string DeathType = null;

		[Desc("Skips the spawned actor's make animations if true.")]
		public readonly bool SkipMakeAnimations = true;

		[Desc("Should an actor only be spawned when the 'Creeps' setting is true?")]
		public readonly bool RequiresLobbyCreeps = false;

		[Desc("Offset of the spawned actor relative to the dying actor's position.",
			"Warning: Spawning an actor outside the parent actor's footprint/influence might",
			"lead to unexpected behaviour.")]
		public readonly CVec Offset = CVec.Zero;

		[Desc("Should an actor spawn after the player has been defeated (e.g. after surrendering)?")]
		public readonly bool SpawnAfterDefeat = true;

		public override object Create(ActorInitializer init) { return new SpawnActorOnDeath(init, this); }
	}