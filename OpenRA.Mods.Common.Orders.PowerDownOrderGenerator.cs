	public class PowerDownOrderGenerator : GlobalButtonOrderGenerator<ToggleConditionOnOrder>
	{
		public PowerDownOrderGenerator()
			: base("PowerDown") { }

		protected override bool IsValidTrait(ToggleConditionOnOrder t)
		{
			return !t.IsTraitDisabled && !t.IsTraitPaused;
		}

		protected override string GetCursor(World world, CPos cell, int2 worldPixel, MouseInput mi)
		{
			mi.Button = MouseButton.Left;
			return OrderInner(world, mi).Any() ? "powerdown" : "powerdown-blocked";
		}
	}