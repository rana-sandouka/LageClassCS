	public abstract class TraitInfo : ITraitInfoInterface
	{
		// Value is set using reflection during TraitInfo creation
		[FieldLoader.Ignore]
		public readonly string InstanceName = null;

		public abstract object Create(ActorInitializer init);
	}