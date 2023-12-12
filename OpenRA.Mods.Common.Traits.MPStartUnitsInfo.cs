	[Desc("Used by SpawnMPUnits. Attach these to the world actor. You can have multiple variants by adding @suffixes.")]
	public class MPStartUnitsInfo : TraitInfo<MPStartUnits>
	{
		[Desc("Internal class ID.")]
		public readonly string Class = "none";

		[Desc("Exposed via the UI to the player.")]
		public readonly string ClassName = "Unlabeled";

		[Desc("Only available when selecting one of these factions.", "Leave empty for no restrictions.")]
		public readonly HashSet<string> Factions = new HashSet<string>();

		[Desc("The actor at the center, usually the mobile construction vehicle.")]
		[ActorReference]
		public readonly string BaseActor = null;

		[Desc("Offset from the spawn point, BaseActor will spawn at.")]
		public readonly CVec BaseActorOffset = CVec.Zero;

		[Desc("A group of units ready to defend or scout.")]
		[ActorReference]
		public readonly string[] SupportActors = { };

		[Desc("Inner radius for spawning support actors")]
		public readonly int InnerSupportRadius = 2;

		[Desc("Outer radius for spawning support actors")]
		public readonly int OuterSupportRadius = 4;

		[Desc("Initial facing of BaseActor. Leave undefined for random facings.")]
		public readonly WAngle? BaseActorFacing = new WAngle(512);

		[Desc("Initial facing of SupportActors. Leave undefined for random facings.")]
		public readonly WAngle? SupportActorsFacing = null;
	}