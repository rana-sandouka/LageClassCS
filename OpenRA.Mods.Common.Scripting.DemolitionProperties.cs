	[ScriptPropertyGroup("Combat")]
	public class DemolitionProperties : ScriptActorProperties, Requires<IMoveInfo>, Requires<DemolitionInfo>
	{
		readonly DemolitionInfo info;

		public DemolitionProperties(ScriptContext context, Actor self)
			: base(context, self)
		{
			info = Self.Info.TraitInfo<DemolitionInfo>();
		}

		[ScriptActorPropertyActivity]
		[Desc("Demolish the target actor.")]
		public void Demolish(Actor target)
		{
			// NB: Scripted actions get no visible targetlines.
			Self.QueueActivity(new Demolish(Self, Target.FromActor(target), info.EnterBehaviour, info.DetonationDelay,
				info.Flashes, info.FlashesDelay, info.FlashInterval, info.DamageTypes, null));
		}
	}