	public abstract class ScriptActorProperties
	{
		protected readonly Actor Self;
		protected readonly ScriptContext Context;
		public ScriptActorProperties(ScriptContext context, Actor self)
		{
			Self = self;
			Context = context;
		}
	}