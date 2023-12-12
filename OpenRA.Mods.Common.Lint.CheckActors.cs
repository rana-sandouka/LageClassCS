	public class CheckActors : ILintMapPass
	{
		public void Run(Action<string> emitError, Action<string> emitWarning, ModData modData, Map map)
		{
			var actorTypes = map.ActorDefinitions.Select(a => a.Value.Value);
			foreach (var actor in actorTypes)
				if (!map.Rules.Actors.Keys.Contains(actor.ToLowerInvariant()))
					emitError("Actor {0} is not defined by any rule.".F(actor));
		}
	}