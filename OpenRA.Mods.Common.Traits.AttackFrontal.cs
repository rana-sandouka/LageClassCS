	public class AttackFrontal : AttackBase
	{
		public new readonly AttackFrontalInfo Info;

		public AttackFrontal(Actor self, AttackFrontalInfo info)
			: base(self, info)
		{
			Info = info;
		}

		protected override bool CanAttack(Actor self, in Target target)
		{
			if (!base.CanAttack(self, target))
				return false;

			return TargetInFiringArc(self, target, Info.FacingTolerance);
		}

		public override Activity GetAttackActivity(Actor self, AttackSource source, in Target newTarget, bool allowMove, bool forceAttack, Color? targetLineColor = null)
		{
			return new Activities.Attack(self, newTarget, allowMove, forceAttack, targetLineColor);
		}
	}