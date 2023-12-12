	public class InaccuracyMultiplier : ConditionalTrait<InaccuracyMultiplierInfo>, IInaccuracyModifier
	{
		public InaccuracyMultiplier(InaccuracyMultiplierInfo info)
			: base(info) { }

		int IInaccuracyModifier.GetInaccuracyModifier() { return IsTraitDisabled ? 100 : Info.Modifier; }
	}