	public class PlayerColorPalette : ILoadsPlayerPalettes
	{
		readonly PlayerColorPaletteInfo info;

		public PlayerColorPalette(PlayerColorPaletteInfo info)
		{
			this.info = info;
		}

		public void LoadPlayerPalettes(WorldRenderer wr, string playerName, Color color, bool replaceExisting)
		{
			var remap = new PlayerColorRemap(info.RemapIndex, color, info.Ramp);
			var pal = new ImmutablePalette(wr.Palette(info.BasePalette).Palette, remap);
			wr.AddPalette(info.BaseName + playerName, pal, info.AllowModifiers, replaceExisting);
		}
	}