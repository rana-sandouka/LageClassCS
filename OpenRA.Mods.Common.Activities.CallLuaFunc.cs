	public sealed class CallLuaFunc : Activity, IDisposable
	{
		readonly ScriptContext context;
		LuaFunction function;

		public CallLuaFunc(LuaFunction function, ScriptContext context)
		{
			this.function = (LuaFunction)function.CopyReference();
			this.context = context;
		}

		public override bool Tick(Actor self)
		{
			try
			{
				function?.Call().Dispose();
			}
			catch (Exception ex)
			{
				context.FatalError(ex.Message);
			}

			Dispose();
			return true;
		}

		public override void Cancel(Actor self, bool keepQueue = false)
		{
			base.Cancel(self, keepQueue);
			Dispose();
			return;
		}

		public void Dispose()
		{
			if (function == null)
				return;

			function.Dispose();
			function = null;
		}
	}