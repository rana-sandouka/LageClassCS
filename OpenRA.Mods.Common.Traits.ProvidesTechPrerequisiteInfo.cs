	public class ProvidesTechPrerequisiteInfo : TraitInfo, ITechTreePrerequisiteInfo
	{
		[Desc("Internal id for this tech level.")]
		public readonly string Id;

		[Translate]
		[Desc("Name shown in the lobby options.")]
		public readonly string Name;

		[Desc("Prerequisites to grant when this tech level is active.")]
		public readonly string[] Prerequisites = { };

		IEnumerable<string> ITechTreePrerequisiteInfo.Prerequisites(ActorInfo info) { return Prerequisites; }

		public override object Create(ActorInitializer init) { return new ProvidesTechPrerequisite(this, init); }
	}