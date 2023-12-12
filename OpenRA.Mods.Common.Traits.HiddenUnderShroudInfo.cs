	[Desc("The actor stays invisible under the shroud.")]
	public class HiddenUnderShroudInfo : TraitInfo, IDefaultVisibilityInfo
	{
		[Desc("Players with these relationships can always see the actor.")]
		public readonly PlayerRelationship AlwaysVisibleRelationships = PlayerRelationship.Ally;

		[Desc("Possible values are CenterPosition (reveal when the center is visible) and ",
			"Footprint (reveal when any footprint cell is visible).")]
		public readonly VisibilityType Type = VisibilityType.Footprint;

		public override object Create(ActorInitializer init) { return new HiddenUnderShroud(this); }
	}