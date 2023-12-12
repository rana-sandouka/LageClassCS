	[Desc("Manages AI repairing base buildings.")]
	public class BuildingRepairBotModuleInfo : ConditionalTraitInfo
	{
		public override object Create(ActorInitializer init) { return new BuildingRepairBotModule(init.Self, this); }
	}