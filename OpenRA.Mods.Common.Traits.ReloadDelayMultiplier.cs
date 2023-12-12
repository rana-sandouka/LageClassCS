	public class ReloadDelayMultiplier : ConditionalTrait<ReloadDelayMultiplierInfo>, IReloadModifier
	{
		public ReloadDelayMultiplier(ReloadDelayMultiplierInfo info)
			: base(info) { }

		int IReloadModifier.GetReloadModifier() { return IsTraitDisabled ? 100 : Info.Modifier; }
	}