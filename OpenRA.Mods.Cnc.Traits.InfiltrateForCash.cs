	class InfiltrateForCash : INotifyInfiltrated
	{
		readonly InfiltrateForCashInfo info;

		public InfiltrateForCash(InfiltrateForCashInfo info) { this.info = info; }

		void INotifyInfiltrated.Infiltrated(Actor self, Actor infiltrator, BitSet<TargetableType> types)
		{
			if (!info.Types.Overlaps(types))
				return;

			var targetResources = self.Owner.PlayerActor.Trait<PlayerResources>();
			var spyResources = infiltrator.Owner.PlayerActor.Trait<PlayerResources>();
			var spyValue = infiltrator.Info.TraitInfoOrDefault<ValuedInfo>();

			var toTake = Math.Min(info.Maximum, (targetResources.Cash + targetResources.Resources) * info.Percentage / 100);
			var toGive = Math.Max(toTake, info.Minimum >= 0 ? info.Minimum : spyValue != null ? spyValue.Cost : 0);

			targetResources.TakeCash(toTake);
			spyResources.GiveCash(toGive);

			if (info.InfiltratedNotification != null)
				Game.Sound.PlayNotification(self.World.Map.Rules, self.Owner, "Speech", info.InfiltratedNotification, self.Owner.Faction.InternalName);

			if (info.InfiltrationNotification != null)
				Game.Sound.PlayNotification(self.World.Map.Rules, infiltrator.Owner, "Speech", info.InfiltrationNotification, infiltrator.Owner.Faction.InternalName);

			if (info.ShowTicks)
				self.World.AddFrameEndTask(w => w.Add(new FloatingText(self.CenterPosition, infiltrator.Owner.Color, FloatingText.FormatCashTick(toGive), 30)));
		}
	}