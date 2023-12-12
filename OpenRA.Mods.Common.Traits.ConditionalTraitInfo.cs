	public abstract class ConditionalTraitInfo : TraitInfo, IObservesVariablesInfo, IRulesetLoaded
	{
		[ConsumedConditionReference]
		[Desc("Boolean expression defining the condition to enable this trait.")]
		public readonly BooleanExpression RequiresCondition = null;

		// HACK: A shim for all the ActorPreview code that used to query UpgradeMinEnabledLevel directly
		// This can go away after we introduce an InitialConditions ActorInit and have the traits query the
		// condition directly
		public bool EnabledByDefault { get; private set; }

		public virtual void RulesetLoaded(Ruleset rules, ActorInfo ai)
		{
			EnabledByDefault = RequiresCondition == null || RequiresCondition.Evaluate(VariableExpression.NoVariables);
		}
	}