	[Desc("Spawns an actor that stays for a limited amount of time.")]
	public class SpawnActorPowerInfo : SupportPowerInfo
	{
		[ActorReference]
		[FieldLoader.Require]
		[Desc("Actor to spawn.")]
		public readonly string Actor = null;

		[Desc("Amount of time to keep the actor alive in ticks. Value < 0 means this actor will not remove itself.")]
		public readonly int LifeTime = 250;

		public readonly string DeploySound = null;

		public readonly string EffectImage = null;

		[SequenceReference(nameof(EffectImage))]
		public readonly string EffectSequence = null;

		[PaletteReference]
		public readonly string EffectPalette = null;

		public override object Create(ActorInitializer init) { return new SpawnActorPower(init.Self, this); }
	}