	public class GrantConditionOnMovement : ConditionalTrait<GrantConditionOnMovementInfo>, INotifyMoving
	{
		readonly IMove movement;
		int conditionToken = Actor.InvalidConditionToken;

		public GrantConditionOnMovement(Actor self, GrantConditionOnMovementInfo info)
			: base(info)
		{
			movement = self.Trait<IMove>();
		}

		void UpdateCondition(Actor self, MovementType types)
		{
			var validMovement = !IsTraitDisabled && (types & Info.ValidMovementTypes) != 0;

			if (!validMovement && conditionToken != Actor.InvalidConditionToken)
				conditionToken = self.RevokeCondition(conditionToken);
			else if (validMovement && conditionToken == Actor.InvalidConditionToken)
				conditionToken = self.GrantCondition(Info.Condition);
		}

		void INotifyMoving.MovementTypeChanged(Actor self, MovementType types)
		{
			UpdateCondition(self, types);
		}

		protected override void TraitEnabled(Actor self)
		{
			UpdateCondition(self, movement.CurrentMovementTypes);
		}

		protected override void TraitDisabled(Actor self)
		{
			UpdateCondition(self, movement.CurrentMovementTypes);
		}
	}