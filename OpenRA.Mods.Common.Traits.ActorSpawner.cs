	public class ActorSpawner : ConditionalTrait<ActorSpawnerInfo>
	{
		public ActorSpawner(ActorSpawnerInfo info)
			: base(info) { }

		public HashSet<string> Types { get { return Info.Types; } }
	}