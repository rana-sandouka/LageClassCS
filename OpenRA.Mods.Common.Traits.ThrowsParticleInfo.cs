	class ThrowsParticleInfo : TraitInfo, Requires<WithSpriteBodyInfo>, Requires<BodyOrientationInfo>
	{
		[FieldLoader.Require]
		public readonly string Anim = null;

		[Desc("Initial position relative to body")]
		public readonly WVec Offset = WVec.Zero;

		[Desc("Minimum distance to throw the particle")]
		public readonly WDist MinThrowRange = new WDist(256);

		[Desc("Maximum distance to throw the particle")]
		public readonly WDist MaxThrowRange = new WDist(768);

		[Desc("Minimum angle to throw the particle")]
		public readonly WAngle MinThrowAngle = WAngle.FromDegrees(30);

		[Desc("Maximum angle to throw the particle")]
		public readonly WAngle MaxThrowAngle = WAngle.FromDegrees(60);

		[Desc("Speed to throw the particle (horizontal WPos/tick)")]
		public readonly int Velocity = 75;

		[Desc("Speed at which the particle turns.")]
		public readonly WAngle TurnSpeed = new WAngle(60);

		public override object Create(ActorInitializer init) { return new ThrowsParticle(init, this); }
	}