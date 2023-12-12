	public class GameSaveLoadingLogic : ChromeLogic
	{
		[ObjectCreator.UseCtor]
		public GameSaveLoadingLogic(Widget widget, ModData modData, World world)
		{
			widget.Get<ProgressBarWidget>("PROGRESS").GetPercentage = () => world.GameSaveLoadingPercentage;

			var versionLabel = widget.GetOrNull<LabelWidget>("VERSION_LABEL");
			if (versionLabel != null)
				versionLabel.Text = modData.Manifest.Metadata.Version;

			var keyhandler = widget.Get<LogicKeyListenerWidget>("CANCEL_HANDLER");
			keyhandler.AddHandler(e =>
			{
				if (e.Event == KeyInputEvent.Down && e.Key == Keycode.ESCAPE)
				{
					Game.Disconnect();
					Ui.ResetAll();
					Game.LoadShellMap();
					return true;
				}

				return false;
			});

			Game.HideCursor = true;
		}

		protected override void Dispose(bool disposing)
		{
			Game.HideCursor = false;
		}
	}