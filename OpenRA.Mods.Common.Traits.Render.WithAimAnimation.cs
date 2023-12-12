	public class WithAimAnimation : ConditionalTrait<WithAimAnimationInfo>, INotifyAiming
	{
		readonly AttackBase attack;
		readonly WithSpriteBody wsb;

		public WithAimAnimation(ActorInitializer init, WithAimAnimationInfo info)
			: base(info)
		{
			attack = init.Self.Trait<AttackBase>();
			wsb = init.Self.TraitsImplementing<WithSpriteBody>().First(w => w.Info.Name == Info.Body);
		}

		protected void UpdateSequence()
		{
			var seq = !IsTraitDisabled && attack.IsAiming ? Info.Sequence : wsb.Info.Sequence;
			wsb.DefaultAnimation.ReplaceAnim(seq);
		}

		void INotifyAiming.StartedAiming(Actor self, AttackBase ab) { UpdateSequence(); }
		void INotifyAiming.StoppedAiming(Actor self, AttackBase ab) { UpdateSequence(); }
		protected override void TraitEnabled(Actor self) { UpdateSequence(); }
		protected override void TraitDisabled(Actor self) { UpdateSequence(); }
	}