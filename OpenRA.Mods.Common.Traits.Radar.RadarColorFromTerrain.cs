	public class RadarColorFromTerrain : IRadarColorModifier
	{
		readonly Color c;

		public RadarColorFromTerrain(Actor self, RadarColorFromTerrainInfo info)
		{
			c = info.GetColorFromTerrain(self.World);
		}

		public bool VisibleOnRadar(Actor self) { return true; }
		public Color RadarColorOverride(Actor self, Color color) { return c; }
	}