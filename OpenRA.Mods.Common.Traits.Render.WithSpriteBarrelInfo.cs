	[Desc("Renders barrels for units with the Turreted trait.")]
	public class WithSpriteBarrelInfo : ConditionalTraitInfo, IRenderActorPreviewSpritesInfo, Requires<TurretedInfo>,
		Requires<ArmamentInfo>, Requires<RenderSpritesInfo>, Requires<BodyOrientationInfo>
	{
		[SequenceReference]
		[Desc("Sequence name to use.")]
		public readonly string Sequence = "barrel";

		[Desc("Armament to use for recoil.")]
		public readonly string Armament = "primary";

		[Desc("Visual offset.")]
		public readonly WVec LocalOffset = WVec.Zero;

		public override object Create(ActorInitializer init) { return new WithSpriteBarrel(init.Self, this); }

		public IEnumerable<IActorPreview> RenderPreviewSprites(ActorPreviewInitializer init, RenderSpritesInfo rs, string image, int facings, PaletteReference p)
		{
			if (!EnabledByDefault)
				yield break;

			var body = init.Actor.TraitInfo<BodyOrientationInfo>();
			var armament = init.Actor.TraitInfos<ArmamentInfo>()
				.First(a => a.Name == Armament);
			var t = init.Actor.TraitInfos<TurretedInfo>()
				.First(tt => tt.Turret == armament.Turret);

			var turretFacing = t.WorldFacingFromInit(init);
			var anim = new Animation(init.World, image, turretFacing);
			anim.Play(RenderSprites.NormalizeSequence(anim, init.GetDamageState(), Sequence));

			var facing = init.GetFacing();
			Func<WRot> orientation = () => body.QuantizeOrientation(WRot.FromYaw(facing()), facings);
			Func<WVec> turretOffset = () => body.LocalToWorld(t.Offset.Rotate(orientation()));
			Func<int> zOffset = () =>
			{
				var tmpOffset = turretOffset();
				return -(tmpOffset.Y + tmpOffset.Z) + 1;
			};

			yield return new SpriteActorPreview(anim, turretOffset, zOffset, p, rs.Scale);
		}
	}