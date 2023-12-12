	public class ReloadAmmoPool : PausableConditionalTrait<ReloadAmmoPoolInfo>, ITick, INotifyAttack, ISync
	{
		AmmoPool ammoPool;
		IReloadAmmoModifier[] modifiers;

		[Sync]
		int remainingTicks;

		public ReloadAmmoPool(ReloadAmmoPoolInfo info)
			: base(info) { }

		protected override void Created(Actor self)
		{
			ammoPool = self.TraitsImplementing<AmmoPool>().Single(ap => ap.Info.Name == Info.AmmoPool);
			modifiers = self.TraitsImplementing<IReloadAmmoModifier>().ToArray();
			base.Created(self);

			self.World.AddFrameEndTask(w =>
			{
				remainingTicks = Util.ApplyPercentageModifiers(Info.Delay, modifiers.Select(m => m.GetReloadAmmoModifier()));
			});
		}

		void INotifyAttack.Attacking(Actor self, in Target target, Armament a, Barrel barrel)
		{
			if (Info.ResetOnFire)
				remainingTicks = Util.ApplyPercentageModifiers(Info.Delay, modifiers.Select(m => m.GetReloadAmmoModifier()));
		}

		void INotifyAttack.PreparingAttack(Actor self, in Target target, Armament a, Barrel barrel) { }

		void ITick.Tick(Actor self)
		{
			if (IsTraitPaused || IsTraitDisabled)
				return;

			Reload(self, Info.Delay, Info.Count, Info.Sound);
		}

		protected virtual void Reload(Actor self, int reloadDelay, int reloadCount, string sound)
		{
			if (!ammoPool.HasFullAmmo && --remainingTicks == 0)
			{
				remainingTicks = Util.ApplyPercentageModifiers(reloadDelay, modifiers.Select(m => m.GetReloadAmmoModifier()));
				if (!string.IsNullOrEmpty(sound))
					Game.Sound.PlayToPlayer(SoundType.World, self.Owner, sound, self.CenterPosition);

				ammoPool.GiveAmmo(self, reloadCount);
			}
		}
	}