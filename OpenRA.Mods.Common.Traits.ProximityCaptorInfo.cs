	[Desc("Actor can capture ProximityCapturable actors.")]
	public class ProximityCaptorInfo : TraitInfo<ProximityCaptor>
	{
		[FieldLoader.Require]
		public readonly BitSet<CaptureType> Types = default(BitSet<CaptureType>);
	}