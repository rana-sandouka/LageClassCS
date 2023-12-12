	public class GrantConditionOnHealth : INotifyCreated, INotifyDamage
	{
		readonly GrantConditionOnHealthInfo info;
		readonly IHealth health;
		readonly int maxHP;

		int conditionToken = Actor.InvalidConditionToken;

		public GrantConditionOnHealth(Actor self, GrantConditionOnHealthInfo info)
		{
			this.info = info;
			health = self.Trait<IHealth>();
			maxHP = info.MaxHP > 0 ? info.MaxHP : health.MaxHP;
		}

		void INotifyCreated.Created(Actor self)
		{
			GrantConditionOnValidHealth(self, health.HP);
		}

		void GrantConditionOnValidHealth(Actor self, int hp)
		{
			if (info.MinHP > hp || maxHP < hp || conditionToken != Actor.InvalidConditionToken)
				return;

			conditionToken = self.GrantCondition(info.Condition);

			var sound = info.EnabledSounds.RandomOrDefault(Game.CosmeticRandom);
			Game.Sound.Play(SoundType.World, sound, self.CenterPosition);
		}

		void INotifyDamage.Damaged(Actor self, AttackInfo e)
		{
			var granted = conditionToken != Actor.InvalidConditionToken;
			if (granted && info.GrantPermanently)
				return;

			if (!granted)
				GrantConditionOnValidHealth(self, health.HP);
			else if (granted && (info.MinHP > health.HP || maxHP < health.HP))
			{
				conditionToken = self.RevokeCondition(conditionToken);

				var sound = info.DisabledSounds.RandomOrDefault(Game.CosmeticRandom);
				Game.Sound.Play(SoundType.World, sound, self.CenterPosition);
			}
		}
	}