	[Desc("A player with this trait will receive a free harvester when his last one gets eaten by a sandworm, provided he has at least one refinery.")]
	public class HarvesterInsuranceInfo : TraitInfo
	{
		public override object Create(ActorInitializer init) { return new HarvesterInsurance(init.Self); }
	}