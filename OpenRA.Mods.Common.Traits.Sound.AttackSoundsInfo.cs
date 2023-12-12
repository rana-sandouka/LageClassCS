	[Desc("Played when preparing for an attack or attacking.")]
	public class AttackSoundsInfo : ConditionalTraitInfo
	{
		[Desc("Play a randomly selected sound from this list when preparing for an attack or attacking.")]
		public readonly string[] Sounds = { };

		[Desc("Delay in ticks before sound starts, either relative to attack preparation or attack.")]
		public readonly int Delay = 0;

		[Desc("Should the sound be delayed relative to preparation or actual attack?")]
		public readonly AttackDelayType DelayRelativeTo = AttackDelayType.Preparation;

		public override object Create(ActorInitializer init) { return new AttackSounds(init, this); }
	}