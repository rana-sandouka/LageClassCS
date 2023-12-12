	public class DoorPlaceholder {
		private Variant type = Variant.Empty;
		
		public Variant Type {
			get => type;

			set {
				if (value.CompareTo(type) > 0) {
					type = value;
				}
			}
		}
		
		public int X;
		public int Y;
		
		public DoorPlaceholder(Dot P) {
			X = P.X;
			Y = P.Y;
		}

		public enum Variant {
			Empty,
			Regular,
			Maze,
			Enemy,
			Locked,
			Treasure,
			Shop,
			Red,
			Challenge,
			Spiked,
			Scourged,
			Portal,
			Payed,
			Head,
			Boss,
			Secret,
			Hidden
		}
	}