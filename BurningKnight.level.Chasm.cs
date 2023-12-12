	public class Chasm : Entity, CollisionFilterEntity {
		public Level Level;

		public override void AddComponents() {
			base.AddComponents();
			AddComponent(new ChasmBodyComponent());
		}

		public bool ShouldCollide(Entity entity) {
			return !entity.TryGetComponent<SupportableComponent>(out var t) || t.Supports.Count == 0;
		}
	}