	public class AsepriteCel : IUserData {
		public AsepriteLayer Layer;

		public Color[] Pixels;

		public int X;
		public int Y;
		public int Width;
		public int Height;
		public float Opacity;

		public string UserDataText { get; set; }
		public Color UserDataColor { get; set; }
	}