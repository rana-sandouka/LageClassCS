	[ScriptPropertyGroup("Cloak")]
	public class CloakProperties : ScriptActorProperties, Requires<CloakInfo>
	{
		readonly Cloak[] cloaks;

		public CloakProperties(ScriptContext context, Actor self)
			: base(context, self)
		{
			cloaks = self.TraitsImplementing<Cloak>().ToArray();
		}

		[Desc("Returns true if the actor is cloaked.")]
		public bool IsCloaked
		{
			get
			{
				return cloaks.Any(c => c.Cloaked);
			}
		}
	}