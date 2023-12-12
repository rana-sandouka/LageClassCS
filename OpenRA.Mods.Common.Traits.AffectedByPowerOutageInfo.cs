	[Desc("Disables the actor when a power outage is triggered (see `InfiltrateForPowerOutage` for more information).")]
	public class AffectedByPowerOutageInfo : ConditionalTraitInfo
	{
		[GrantedConditionReference]
		[Desc("The condition to grant while there is a power outage.")]
		public readonly string Condition = null;

		public override object Create(ActorInitializer init) { return new AffectedByPowerOutage(init.Self, this); }
	}