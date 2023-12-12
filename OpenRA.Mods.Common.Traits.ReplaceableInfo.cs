	public class ReplaceableInfo : ConditionalTraitInfo
	{
		[FieldLoader.Require]
		[Desc("Replacement types this Replaceable actor accepts.")]
		public readonly HashSet<string> Types = new HashSet<string>();

		public override object Create(ActorInitializer init) { return new Replaceable(this); }
	}