		class Watcher
		{
			public readonly string Key;
			public ITechTreeElement RegisteredBy { get { return watcher; } }

			// Strings may be either actor type, or "alternate name" key
			readonly string[] prerequisites;
			readonly ITechTreeElement watcher;
			bool hasPrerequisites;
			int limit;
			bool hidden;
			bool initialized = false;

			public Watcher(string key, string[] prerequisites, int limit, ITechTreeElement watcher)
			{
				Key = key;
				this.prerequisites = prerequisites;
				this.watcher = watcher;
				hasPrerequisites = false;
				this.limit = limit;
				hidden = false;
			}

			bool HasPrerequisites(Cache<string, List<Actor>> ownedPrerequisites)
			{
				// PERF: Avoid LINQ.
				foreach (var prereq in prerequisites)
				{
					var withoutTilde = prereq.Replace("~", "");
					if (withoutTilde.StartsWith("!", StringComparison.Ordinal) ^ !ownedPrerequisites.ContainsKey(withoutTilde.Replace("!", "")))
						return false;
				}

				return true;
			}

			bool IsHidden(Cache<string, List<Actor>> ownedPrerequisites)
			{
				// PERF: Avoid LINQ.
				foreach (var prereq in prerequisites)
				{
					if (!prereq.StartsWith("~", StringComparison.Ordinal))
						continue;
					var withoutTilde = prereq.Replace("~", "");
					if (withoutTilde.StartsWith("!", StringComparison.Ordinal) ^ !ownedPrerequisites.ContainsKey(withoutTilde.Replace("!", "")))
						return true;
				}

				return false;
			}

			public void Update(Cache<string, List<Actor>> ownedPrerequisites)
			{
				var hasReachedLimit = limit > 0 && ownedPrerequisites.ContainsKey(Key) && ownedPrerequisites[Key].Count >= limit;

				// The '!' annotation inverts prerequisites: "I'm buildable if this prerequisite *isn't* met"
				var nowHasPrerequisites = HasPrerequisites(ownedPrerequisites) && !hasReachedLimit;
				var nowHidden = IsHidden(ownedPrerequisites);

				if (initialized == false)
				{
					initialized = true;
					hasPrerequisites = !nowHasPrerequisites;
					hidden = !nowHidden;
				}

				// Hide the item from the UI if a prereq annotated with '~' is not met.
				if (nowHidden && !hidden)
					watcher.PrerequisitesItemHidden(Key);

				if (!nowHidden && hidden)
					watcher.PrerequisitesItemVisible(Key);

				if (nowHasPrerequisites && !hasPrerequisites)
					watcher.PrerequisitesAvailable(Key);

				if (!nowHasPrerequisites && hasPrerequisites)
					watcher.PrerequisitesUnavailable(Key);

				hidden = nowHidden;
				hasPrerequisites = nowHasPrerequisites;
			}
		}