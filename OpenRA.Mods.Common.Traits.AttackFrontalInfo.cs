	[Desc("Unit got to face the target")]
	public class AttackFrontalInfo : AttackBaseInfo, Requires<IFacingInfo>
	{
		[Desc("Tolerance for attack angle. Range [0, 512], 512 covers 360 degrees.")]
		public readonly new WAngle FacingTolerance = WAngle.Zero;

		public override object Create(ActorInitializer init) { return new AttackFrontal(init.Self, this); }
	}