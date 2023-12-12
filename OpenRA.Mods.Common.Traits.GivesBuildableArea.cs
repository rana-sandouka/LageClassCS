	public class GivesBuildableArea : ConditionalTrait<GivesBuildableAreaInfo>
	{
		public GivesBuildableArea(GivesBuildableAreaInfo info)
			: base(info) { }

		readonly HashSet<string> noAreaTypes = new HashSet<string>();

		public HashSet<string> AreaTypes { get { return !IsTraitDisabled ? Info.AreaTypes : noAreaTypes; } }
	}