	[Desc("Used together with ClassicProductionQueue.")]
	public class PrimaryBuildingInfo : ConditionalTraitInfo
	{
		[GrantedConditionReference]
		[Desc("The condition to grant to self while this is the primary building.")]
		public readonly string PrimaryCondition = null;

		[NotificationReference("Speech")]
		[Desc("The speech notification to play when selecting a primary building.")]
		public readonly string SelectionNotification = null;

		[Desc("List of production queues for which the primary flag should be set.",
			"If empty, the list given in the `Produces` property of the `Production` trait will be used.")]
		public readonly string[] ProductionQueues = { };

		public override object Create(ActorInitializer init) { return new PrimaryBuilding(init.Self, this); }
	}