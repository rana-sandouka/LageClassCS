	public class GrantConditionOnPowerState : ConditionalTrait<GrantConditionOnPowerStateInfo>, INotifyOwnerChanged, INotifyPowerLevelChanged
	{
		PowerManager playerPower;
		int conditionToken = Actor.InvalidConditionToken;

		bool validPowerState;

		public GrantConditionOnPowerState(Actor self, GrantConditionOnPowerStateInfo info)
			: base(info)
		{
			playerPower = self.Owner.PlayerActor.Trait<PowerManager>();
		}

		protected override void Created(Actor self)
		{
			base.Created(self);

			Update(self);
		}

		protected override void TraitEnabled(Actor self)
		{
			Update(self);
		}

		protected override void TraitDisabled(Actor self)
		{
			Update(self);
		}

		void Update(Actor self)
		{
			validPowerState = !IsTraitDisabled && Info.ValidPowerStates.HasFlag(playerPower.PowerState);

			if (validPowerState && conditionToken == Actor.InvalidConditionToken)
				conditionToken = self.GrantCondition(Info.Condition);
			else if (!validPowerState && conditionToken != Actor.InvalidConditionToken)
				conditionToken = self.RevokeCondition(conditionToken);
		}

		void INotifyPowerLevelChanged.PowerLevelChanged(Actor self)
		{
			Update(self);
		}

		void INotifyOwnerChanged.OnOwnerChanged(Actor self, Player oldOwner, Player newOwner)
		{
			playerPower = newOwner.PlayerActor.Trait<PowerManager>();
			Update(self);
		}
	}