	public class TreasureChest : Chest {
		public override string GetSprite() {
			return "treasure_chest";
		}

		protected override void DefineDrops() {
			GetComponent<DropsComponent>().Add(new SimpleDrop(1, 1, 1, "bk:treasure_key"));
		}
	}