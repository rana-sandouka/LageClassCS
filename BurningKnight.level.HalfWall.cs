	public class HalfWall : Entity {
		public Level Level;

		public override void AddComponents() {
			base.AddComponents();
			AddComponent(new HalfWallBodyComponent {
				Level = Level
			});
		}
	}