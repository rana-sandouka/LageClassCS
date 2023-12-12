	public class DefaultInputHandler : IInputHandler
	{
		readonly World world;
		public DefaultInputHandler(World world)
		{
			this.world = world;
		}

		public void ModifierKeys(Modifiers mods)
		{
			Game.HandleModifierKeys(mods);
		}

		public void OnKeyInput(KeyInput input)
		{
			Sync.RunUnsynced(Game.Settings.Debug.SyncCheckUnsyncedCode, world, () => Ui.HandleKeyPress(input));
		}

		public void OnTextInput(string text)
		{
			Sync.RunUnsynced(Game.Settings.Debug.SyncCheckUnsyncedCode, world, () => Ui.HandleTextInput(text));
		}

		public void OnMouseInput(MouseInput input)
		{
			Sync.RunUnsynced(Game.Settings.Debug.SyncCheckUnsyncedCode, world, () => Ui.HandleInput(input));
		}
	}