	[ChromeLogicArgsHotkeys("PauseKey")]
	public class PauseHotkeyLogic : SingleHotkeyBaseLogic
	{
		readonly World world;

		[ObjectCreator.UseCtor]
		public PauseHotkeyLogic(Widget widget, ModData modData, World world, Dictionary<string, MiniYaml> logicArgs)
			: base(widget, modData, "PauseKey", "WORLD_KEYHANDLER", logicArgs)
		{
			this.world = world;
		}

		protected override bool OnHotkeyActivated(KeyInput e)
		{
			// Disable pausing for spectators and defeated players unless they are the game host
			if (!Game.IsHost && (world.LocalPlayer == null || world.LocalPlayer.WinState == WinState.Lost))
				return false;

			world.SetPauseState(!world.Paused);

			return true;
		}
	}