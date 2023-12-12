	public class DamageMultiplier : ConditionalTrait<DamageMultiplierInfo>, IDamageModifier
	{
		public DamageMultiplier(DamageMultiplierInfo info)
			: base(info) { }

		int IDamageModifier.GetDamageModifier(Actor attacker, Damage damage)
		{
			return IsTraitDisabled ? 100 : Info.Modifier;
		}
	}