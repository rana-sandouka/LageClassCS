	[Desc("Reloads an ammo pool.")]
	public class ReloadAmmoPoolInfo : PausableConditionalTraitInfo
	{
		[Desc("Reload ammo pool with this name.")]
		public readonly string AmmoPool = "primary";

		[Desc("Reload time in ticks per Count.")]
		public readonly int Delay = 50;

		[Desc("How much ammo is reloaded after Delay.")]
		public readonly int Count = 1;

		[Desc("Whether or not reload timer should be reset when ammo has been fired.")]
		public readonly bool ResetOnFire = false;

		[Desc("Play this sound each time ammo is reloaded.")]
		public readonly string Sound = null;

		public override object Create(ActorInitializer init) { return new ReloadAmmoPool(this); }

		public override void RulesetLoaded(Ruleset rules, ActorInfo ai)
		{
			if (ai.TraitInfos<AmmoPoolInfo>().Count(ap => ap.Name == AmmoPool) != 1)
				throw new YamlException("ReloadsAmmoPool.AmmoPool requires exactly one AmmoPool with matching Name!");

			base.RulesetLoaded(rules, ai);
		}
	}