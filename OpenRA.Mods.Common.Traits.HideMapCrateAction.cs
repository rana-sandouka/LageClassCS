	class HideMapCrateAction : CrateAction
	{
		readonly HideMapCrateActionInfo info;

		public HideMapCrateAction(Actor self, HideMapCrateActionInfo info)
			: base(self, info)
		{
			this.info = info;
		}

		public override int GetSelectionShares(Actor collector)
		{
			// Don't hide the map if the shroud is force-revealed
			var preventReset = collector.Owner.PlayerActor.TraitsImplementing<IPreventsShroudReset>()
				.Any(p => p.PreventShroudReset(collector.Owner.PlayerActor));
			if (preventReset || collector.Owner.Shroud.ExploreMapEnabled)
				return 0;

			return base.GetSelectionShares(collector);
		}

		public override void Activate(Actor collector)
		{
			if (info.IncludeAllies)
			{
				foreach (var player in collector.World.Players)
					if (player.IsAlliedWith(collector.Owner))
						player.Shroud.ResetExploration();
			}
			else
				collector.Owner.Shroud.ResetExploration();

			base.Activate(collector);
		}
	}