	[ScriptPropertyGroup("Production")]
	public class ProductionQueueProperties : ScriptActorProperties, Requires<ProductionQueueInfo>, Requires<ScriptTriggersInfo>
	{
		readonly ProductionQueue[] queues;
		readonly ScriptTriggers triggers;

		public ProductionQueueProperties(ScriptContext context, Actor self)
			: base(context, self)
		{
			queues = self.TraitsImplementing<ProductionQueue>().Where(q => q.Enabled).ToArray();
			triggers = TriggerGlobal.GetScriptTriggers(self);
		}

		[Desc("Build the specified set of actors using a TD-style (per building) production queue. " +
			"The function will return true if production could be started, false otherwise. " +
			"If an actionFunc is given, it will be called as actionFunc(Actor[] actors) once " +
			"production of all actors has been completed.  The actors array is guaranteed to " +
			"only contain alive actors.")]
		public bool Build(string[] actorTypes, LuaFunction actionFunc = null)
		{
			if (triggers.HasAnyCallbacksFor(Trigger.OnProduction))
				return false;

			var queue = queues.Where(q => actorTypes.All(t => GetBuildableInfo(t).Queue.Contains(q.Info.Type)))
				.FirstOrDefault(q => !q.AllQueued().Any());

			if (queue == null)
				return false;

			if (actionFunc != null)
			{
				var player = Self.Owner;
				var squadSize = actorTypes.Length;
				var squad = new List<Actor>();
				var func = actionFunc.CopyReference() as LuaFunction;

				Action<Actor, Actor> productionHandler = (_, __) => { };
				productionHandler = (factory, unit) =>
				{
					if (player != factory.Owner)
					{
						triggers.OnProducedInternal -= productionHandler;
						return;
					}

					squad.Add(unit);
					if (squad.Count >= squadSize)
					{
						using (func)
						using (var luaSquad = squad.Where(u => !u.IsDead).ToArray().ToLuaValue(Context))
							func.Call(luaSquad).Dispose();

						triggers.OnProducedInternal -= productionHandler;
					}
				};

				triggers.OnProducedInternal += productionHandler;
			}

			foreach (var actorType in actorTypes)
				queue.ResolveOrder(Self, Order.StartProduction(Self, actorType, 1));

			return true;
		}

		[Desc("Check whether the factory's production queue that builds this type of actor is currently busy. " +
			"Note: it does not check whether this particular type of actor is being produced.")]
		public bool IsProducing(string actorType)
		{
			if (triggers.HasAnyCallbacksFor(Trigger.OnProduction))
				return true;

			return queues.Where(q => GetBuildableInfo(actorType).Queue.Contains(q.Info.Type))
				.Any(q => q.AllQueued().Any());
		}

		BuildableInfo GetBuildableInfo(string actorType)
		{
			var ri = Self.World.Map.Rules.Actors[actorType];
			var bi = ri.TraitInfoOrDefault<BuildableInfo>();

			if (bi == null)
				throw new LuaException("Actor of type {0} cannot be produced".F(actorType));
			else
				return bi;
		}
	}