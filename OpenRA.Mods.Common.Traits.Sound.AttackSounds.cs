	public class AttackSounds : ConditionalTrait<AttackSoundsInfo>, INotifyAttack, ITick
	{
		readonly AttackSoundsInfo info;
		int tick;

		public AttackSounds(ActorInitializer init, AttackSoundsInfo info)
			: base(info)
		{
			this.info = info;
		}

		void PlaySound(Actor self)
		{
			if (info.Sounds.Any())
				Game.Sound.Play(SoundType.World, info.Sounds, self.World, self.CenterPosition);
		}

		void INotifyAttack.Attacking(Actor self, in Target target, Armament a, Barrel barrel)
		{
			if (info.DelayRelativeTo == AttackDelayType.Attack)
			{
				if (info.Delay > 0)
					tick = info.Delay;
				else
					PlaySound(self);
			}
		}

		void INotifyAttack.PreparingAttack(Actor self, in Target target, Armament a, Barrel barrel)
		{
			if (info.DelayRelativeTo == AttackDelayType.Preparation)
			{
				if (info.Delay > 0)
					tick = info.Delay;
				else
					PlaySound(self);
			}
		}

		void ITick.Tick(Actor self)
		{
			if (IsTraitDisabled)
				return;

			if (info.Delay > 0 && --tick == 0)
				PlaySound(self);
		}
	}