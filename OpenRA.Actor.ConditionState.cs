		class ConditionState
		{
			/// <summary>Delegates that have registered to be notified when this condition changes.</summary>
			public readonly List<VariableObserverNotifier> Notifiers = new List<VariableObserverNotifier>();

			/// <summary>Unique integers identifying granted instances of the condition.</summary>
			public readonly HashSet<int> Tokens = new HashSet<int>();
		}