	[ChromeLogicArgsHotkeys("MuteAudioKey")]
	public class MuteHotkeyLogic : SingleHotkeyBaseLogic
	{
		[ObjectCreator.UseCtor]
		public MuteHotkeyLogic(Widget widget, ModData modData, Dictionary<string, MiniYaml> logicArgs)
			: base(widget, modData, "MuteAudioKey", "GLOBAL_KEYHANDLER", logicArgs) { }

		protected override bool OnHotkeyActivated(KeyInput e)
		{
			Game.Settings.Sound.Mute ^= true;

			if (Game.Settings.Sound.Mute)
			{
				Game.Sound.MuteAudio();
				Game.AddSystemLine("Audio muted");
			}
			else
			{
				Game.Sound.UnmuteAudio();
				Game.AddSystemLine("Audio unmuted");
			}

			return true;
		}
	}