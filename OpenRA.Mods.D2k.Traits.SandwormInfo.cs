	class SandwormInfo : WandersInfo, Requires<MobileInfo>, Requires<AttackBaseInfo>
	{
		[Desc("Time between rescanning for targets (in ticks).")]
		public readonly int TargetRescanInterval = 125;

		[Desc("The radius in which the worm \"searches\" for targets.")]
		public readonly WDist MaxSearchRadius = WDist.FromCells(20);

		[Desc("The range at which the worm launches an attack regardless of noise levels.")]
		public readonly WDist IgnoreNoiseAttackRange = WDist.FromCells(3);

		[Desc("The chance this actor has of disappearing after it attacks (in %).")]
		public readonly int ChanceToDisappear = 100;

		public override object Create(ActorInitializer init) { return new Sandworm(init.Self, this); }
	}