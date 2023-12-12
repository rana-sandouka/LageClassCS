	public class GiveShieldHappening : Happening {
		public override void Happen(Player player) {
			player.GetComponent<InventoryComponent>().Pickup(Items.CreateAndAdd("bk:shield", player.Area));
		}
	}