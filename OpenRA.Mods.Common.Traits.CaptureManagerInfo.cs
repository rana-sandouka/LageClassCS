	[Desc("Manages Captures and Capturable traits on an actor.")]
	public class CaptureManagerInfo : TraitInfo
	{
		[GrantedConditionReference]
		[Desc("Condition granted when capturing an actor.")]
		public readonly string CapturingCondition = null;

		[GrantedConditionReference]
		[Desc("Condition granted when being captured by another actor.")]
		public readonly string BeingCapturedCondition = null;

		[Desc("Should units friendly to the capturing actor auto-target this actor while it is being captured?")]
		public readonly bool PreventsAutoTarget = true;

		public override object Create(ActorInitializer init) { return new CaptureManager(this); }

		public bool CanBeTargetedBy(FrozenActor frozenActor, Actor captor, Captures captures)
		{
			if (captures.IsTraitDisabled)
				return false;

			// TODO: FrozenActors don't yet have a way of caching conditions, so we can't filter disabled traits
			// This therefore assumes that all Capturable traits are enabled, which is probably wrong.
			// Actors with FrozenUnderFog should therefore not disable the Capturable trait.
			var stance = captor.Owner.RelationshipWith(frozenActor.Owner);
			return frozenActor.Info.TraitInfos<CapturableInfo>()
				.Any(c => c.ValidRelationships.HasStance(stance) && captures.Info.CaptureTypes.Overlaps(c.Types));
		}
	}