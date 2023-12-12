	public class AttackBomber : AttackBase, ITick, ISync, INotifyRemovedFromWorld
	{
		readonly AttackBomberInfo info;

		[Sync]
		Target target;

		[Sync]
		bool inAttackRange;

		[Sync]
		bool facingTarget = true;

		public event Action<Actor> OnRemovedFromWorld = self => { };
		public event Action<Actor> OnEnteredAttackRange = self => { };
		public event Action<Actor> OnExitedAttackRange = self => { };

		public AttackBomber(Actor self, AttackBomberInfo info)
			: base(self, info)
		{
			this.info = info;
		}

		void ITick.Tick(Actor self)
		{
			var wasInAttackRange = inAttackRange;
			inAttackRange = false;

			if (self.IsInWorld)
			{
				var dat = self.World.Map.DistanceAboveTerrain(target.CenterPosition);
				target = Target.FromPos(target.CenterPosition - new WVec(WDist.Zero, WDist.Zero, dat));

				var wasFacingTarget = facingTarget;
				facingTarget = TargetInFiringArc(self, target, info.FacingTolerance);

				foreach (var a in Armaments)
				{
					if (!target.IsInRange(self.CenterPosition, a.MaxRange()))
						continue;

					inAttackRange = true;
					a.CheckFire(self, facing, target);
				}

				// Actors without armaments may want to trigger an action when it passes the target
				if (!Armaments.Any())
					inAttackRange = !wasInAttackRange && !facingTarget && wasFacingTarget;
			}

			if (inAttackRange && !wasInAttackRange)
				OnEnteredAttackRange(self);

			if (!inAttackRange && wasInAttackRange)
				OnExitedAttackRange(self);
		}

		public void SetTarget(World w, WPos pos) { target = Target.FromPos(pos); }

		void INotifyRemovedFromWorld.RemovedFromWorld(Actor self)
		{
			OnRemovedFromWorld(self);
		}

		public override Activity GetAttackActivity(Actor self, AttackSource source, in Target newTarget, bool allowMove, bool forceAttack, Color? targetLineColor)
		{
			throw new NotImplementedException("AttackBomber requires a scripted target");
		}
	}