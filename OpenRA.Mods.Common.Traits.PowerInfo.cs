	public class PowerInfo : ConditionalTraitInfo
	{
		[Desc("If negative, it will drain power. If positive, it will provide power.")]
		public readonly int Amount = 0;

		public override object Create(ActorInitializer init) { return new Power(init.Self, this); }
	}