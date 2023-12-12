	[Desc("Reveals a decoration sprite to the indicated players when infiltrated.")]
	class InfiltrateForDecorationInfo : WithDecorationInfo
	{
		[Desc("The `TargetTypes` from `Targetable` that are allowed to enter.")]
		public readonly BitSet<TargetableType> Types = default(BitSet<TargetableType>);

		public override object Create(ActorInitializer init) { return new InfiltrateForDecoration(init.Self, this); }
	}