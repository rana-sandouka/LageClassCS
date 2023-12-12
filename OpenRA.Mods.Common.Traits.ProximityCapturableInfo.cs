	[Desc("Actor can be captured by units in a specified proximity.")]
	public class ProximityCapturableInfo : TraitInfo, IRulesetLoaded
	{
		[Desc("Maximum range at which a ProximityCaptor actor can initiate the capture.")]
		public readonly WDist Range = WDist.FromCells(5);

		[Desc("Allowed ProximityCaptor actors to capture this actor.")]
		public readonly BitSet<CaptureType> CaptorTypes = new BitSet<CaptureType>("Player", "Vehicle", "Tank", "Infantry");

		[Desc("If set, the capturing process stops immediately after another player comes into Range.")]
		public readonly bool MustBeClear = false;

		[Desc("If set, the ownership will not revert back when the captor leaves the area.")]
		public readonly bool Sticky = false;

		[Desc("If set, the actor can only be captured via this logic once.",
			"This option implies the `Sticky` behaviour as well.")]
		public readonly bool Permanent = false;

		public void RulesetLoaded(Ruleset rules, ActorInfo info)
		{
			var pci = rules.Actors["player"].TraitInfoOrDefault<ProximityCaptorInfo>();
			if (pci == null)
				throw new YamlException("ProximityCapturable requires the `Player` actor to have the ProximityCaptor trait.");
		}

		public override object Create(ActorInitializer init) { return new ProximityCapturable(init.Self, this); }
	}