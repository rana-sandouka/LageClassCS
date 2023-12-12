	public class WorldViewportSizes : IGlobalModData
	{
		public readonly int2 CloseWindowHeights = new int2(480, 600);
		public readonly int2 MediumWindowHeights = new int2(600, 900);
		public readonly int2 FarWindowHeights = new int2(900, 1300);

		public readonly float MaxZoomScale = 2.0f;
		public readonly int MaxZoomWindowHeight = 240;
		public readonly bool AllowNativeZoom = true;

		public readonly Size MinEffectiveResolution = new Size(1024, 720);

		public int2 GetSizeRange(WorldViewport distance)
		{
			return distance == WorldViewport.Close ? CloseWindowHeights
				: distance == WorldViewport.Medium ? MediumWindowHeights
				: FarWindowHeights;
		}
	}