	public class GrannyDecor : SolidProp {
		public GrannyDecor() {
			Sprite = "granny";
		}
		
		protected override Rectangle GetCollider() {
			return new Rectangle(0, 8, 60, 14);
		}
	}