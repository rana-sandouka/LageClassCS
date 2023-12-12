	public class BeaconOrderButtonLogic : ChromeLogic
	{
		[ObjectCreator.UseCtor]
		public BeaconOrderButtonLogic(Widget widget, World world)
		{
			var beacon = widget as ButtonWidget;
			if (beacon != null)
				OrderButtonsChromeUtils.BindOrderButton<BeaconOrderGenerator>(world, beacon, "beacon");
		}
	}