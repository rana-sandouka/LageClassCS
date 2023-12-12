		public class PostRollState : EntityState {
			public override void Update(float dt) {
				base.Update(dt);
				
				if (T >= 0.2f) {
					Self.GetComponent<RectBodyComponent>().Acceleration = Vector2.Zero;
					Become<IdleState>();
				}
			}
		}