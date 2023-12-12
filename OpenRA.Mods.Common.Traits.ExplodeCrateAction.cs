	class ExplodeCrateAction : CrateAction
	{
		readonly ExplodeCrateActionInfo info;

		public ExplodeCrateAction(Actor self, ExplodeCrateActionInfo info)
			: base(self, info)
		{
			this.info = info;
		}

		public override void Activate(Actor collector)
		{
			var weapon = collector.World.Map.Rules.Weapons[info.Weapon.ToLowerInvariant()];
			weapon.Impact(Target.FromPos(collector.CenterPosition), collector);

			base.Activate(collector);
		}
	}