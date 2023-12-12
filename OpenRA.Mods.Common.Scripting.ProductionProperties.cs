	[ScriptPropertyGroup("Production")]
	public class ProductionProperties : ScriptActorProperties, Requires<ProductionInfo>
	{
		readonly Production[] productionTraits;

		public ProductionProperties(ScriptContext context, Actor self)
			: base(context, self)
		{
			productionTraits = self.TraitsImplementing<Production>().ToArray();
		}

		[ScriptActorPropertyActivity]
		[Desc("Build a unit, ignoring the production queue. The activity will wait if the exit is blocked.",
			"If productionType is nil or unavailable, then an exit will be selected based on 'Buildable.BuildAtProductionType'.",
			"If 'Buildable.BuildAtProductionType' is not set either, a random exit will be selected.")]
		public void Produce(string actorType, string factionVariant = null, string productionType = null)
		{
			if (!Self.World.Map.Rules.Actors.TryGetValue(actorType, out var actorInfo))
				throw new LuaException("Unknown actor type '{0}'".F(actorType));

			var bi = actorInfo.TraitInfo<BuildableInfo>();
			Self.QueueActivity(new WaitFor(() =>
			{
				// Go through all available traits and see which one successfully produces
				foreach (var p in productionTraits)
				{
					var type = productionType ?? bi.BuildAtProductionType;
					if (!string.IsNullOrEmpty(type) && !p.Info.Produces.Contains(type))
						continue;

					var inits = new TypeDictionary
					{
						new OwnerInit(Self.Owner),
						new FactionInit(factionVariant ?? BuildableInfo.GetInitialFaction(actorInfo, p.Faction))
					};

					if (p.Produce(Self, actorInfo, type, inits, 0))
						return true;
				}

				// We didn't produce anything, wait until we do
				return false;
			}));
		}
	}