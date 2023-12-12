	public class WithSpriteBody : PausableConditionalTrait<WithSpriteBodyInfo>, INotifyDamageStateChanged, IAutoMouseBounds
	{
		public readonly Animation DefaultAnimation;
		readonly RenderSprites rs;
		readonly Animation boundsAnimation;

		public WithSpriteBody(ActorInitializer init, WithSpriteBodyInfo info)
			: this(init, info, () => WAngle.Zero) { }

		protected WithSpriteBody(ActorInitializer init, WithSpriteBodyInfo info, Func<WAngle> baseFacing)
			: base(info)
		{
			rs = init.Self.Trait<RenderSprites>();

			Func<bool> paused = () => IsTraitPaused &&
				DefaultAnimation.CurrentSequence.Name == NormalizeSequence(init.Self, Info.Sequence);

			Func<WVec> subtractDAT = null;
			if (info.ForceToGround)
				subtractDAT = () => new WVec(0, 0, -init.Self.World.Map.DistanceAboveTerrain(init.Self.CenterPosition).Length);

			DefaultAnimation = new Animation(init.World, rs.GetImage(init.Self), baseFacing, paused);
			rs.Add(new AnimationWithOffset(DefaultAnimation, subtractDAT, () => IsTraitDisabled));

			// Cache the bounds from the default sequence to avoid flickering when the animation changes
			boundsAnimation = new Animation(init.World, rs.GetImage(init.Self), baseFacing, paused);
			boundsAnimation.PlayRepeating(info.Sequence);
		}

		public string NormalizeSequence(Actor self, string sequence)
		{
			return RenderSprites.NormalizeSequence(DefaultAnimation, self.GetDamageState(), sequence);
		}

		protected override void TraitEnabled(Actor self)
		{
			if (Info.StartSequence != null)
				PlayCustomAnimation(self, Info.StartSequence,
					() => DefaultAnimation.PlayRepeating(NormalizeSequence(self, Info.Sequence)));
			else
				DefaultAnimation.PlayRepeating(NormalizeSequence(self, Info.Sequence));
		}

		public virtual void PlayCustomAnimation(Actor self, string name, Action after = null)
		{
			DefaultAnimation.PlayThen(NormalizeSequence(self, name), () =>
			{
				CancelCustomAnimation(self);
				after?.Invoke();
			});
		}

		public virtual void PlayCustomAnimationRepeating(Actor self, string name)
		{
			DefaultAnimation.PlayRepeating(NormalizeSequence(self, name));
		}

		public virtual void PlayCustomAnimationBackwards(Actor self, string name, Action after = null)
		{
			DefaultAnimation.PlayBackwardsThen(NormalizeSequence(self, name), () =>
			{
				CancelCustomAnimation(self);
				after?.Invoke();
			});
		}

		public virtual void CancelCustomAnimation(Actor self)
		{
			DefaultAnimation.PlayRepeating(NormalizeSequence(self, Info.Sequence));
		}

		protected virtual void DamageStateChanged(Actor self)
		{
			if (DefaultAnimation.CurrentSequence != null)
				DefaultAnimation.ReplaceAnim(NormalizeSequence(self, DefaultAnimation.CurrentSequence.Name));
		}

		void INotifyDamageStateChanged.DamageStateChanged(Actor self, AttackInfo e)
		{
			DamageStateChanged(self);
		}

		Rectangle IAutoMouseBounds.AutoMouseoverBounds(Actor self, WorldRenderer wr)
		{
			return boundsAnimation.ScreenBounds(wr, self.CenterPosition, WVec.Zero, rs.Info.Scale);
		}
	}