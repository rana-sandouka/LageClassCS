	[ScriptPropertyGroup("Ability")]
	public class DeliversExperienceProperties : ScriptActorProperties, Requires<IMoveInfo>, Requires<DeliversExperienceInfo>
	{
		readonly DeliversExperienceInfo deliversExperience;
		readonly GainsExperience gainsExperience;

		public DeliversExperienceProperties(ScriptContext context, Actor self)
			: base(context, self)
		{
			deliversExperience = Self.Info.TraitInfo<DeliversExperienceInfo>();
			gainsExperience = Self.Trait<GainsExperience>();
		}

		[ScriptActorPropertyActivity]
		[Desc("Deliver experience to the target actor.")]
		public void DeliverExperience(Actor target)
		{
			var targetGainsExperience = target.TraitOrDefault<GainsExperience>();
			if (targetGainsExperience == null)
				throw new LuaException("Actor '{0}' cannot gain experience!".F(target));

			if (targetGainsExperience.Level == targetGainsExperience.MaxLevel)
				return;

			var level = gainsExperience.Level;
			var t = Target.FromActor(target);

			// NB: Scripted actions get no visible targetlines.
			Self.QueueActivity(new DonateExperience(Self, t, level, deliversExperience.PlayerExperience, null));
		}
	}