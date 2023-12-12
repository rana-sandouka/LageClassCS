		class ProximityTrigger : IDisposable
		{
			public WPos TopLeft { get; private set; }
			public WPos BottomRight { get; private set; }

			public bool Dirty;

			readonly Action<Actor> onActorEntered;
			readonly Action<Actor> onActorExited;
			readonly HashSet<Actor> oldActors = new HashSet<Actor>();
			readonly HashSet<Actor> currentActors = new HashSet<Actor>();

			WPos position;
			WDist range;
			WDist vRange;

			public ProximityTrigger(WPos pos, WDist range, WDist vRange, Action<Actor> onActorEntered, Action<Actor> onActorExited)
			{
				this.onActorEntered = onActorEntered;
				this.onActorExited = onActorExited;

				Update(pos, range, vRange);
			}

			public void Update(WPos newPos, WDist newRange, WDist newVRange)
			{
				position = newPos;
				range = newRange;
				vRange = newVRange;

				var offset = new WVec(newRange, newRange, newVRange);

				TopLeft = newPos - offset;
				BottomRight = newPos + offset;

				Dirty = true;
			}

			public void Tick(ActorMap am)
			{
				if (!Dirty)
					return;

				// PERF: Reuse collection to avoid allocations.
				oldActors.Clear();
				oldActors.UnionWith(currentActors);

				var delta = new WVec(range, range, WDist.Zero);
				currentActors.Clear();
				currentActors.UnionWith(
					am.ActorsInBox(position - delta, position + delta)
					.Where(a => (a.CenterPosition - position).HorizontalLengthSquared < range.LengthSquared
						&& (vRange.Length == 0 || (a.World.Map.DistanceAboveTerrain(a.CenterPosition).LengthSquared <= vRange.LengthSquared))));

				if (onActorEntered != null)
					foreach (var a in currentActors)
						if (!oldActors.Contains(a))
							onActorEntered(a);

				if (onActorExited != null)
					foreach (var a in oldActors)
						if (!currentActors.Contains(a))
							onActorExited(a);

				Dirty = false;
			}

			public void Dispose()
			{
				if (onActorExited != null)
					foreach (var a in currentActors)
						onActorExited(a);
			}
		}