	public class MakeBombsHomeUse : ItemUse {
		private float speed;
		
		public override bool HandleEvent(Event e) {
			if (e is BombPlacedEvent bce) {
				bce.Bomb.ExplodeOnTouch = true;
				bce.Bomb.Controller += TargetBombController.Make(null, speed);
			}
			
			return base.HandleEvent(e);
		}

		public override void Setup(JsonValue settings) {
			base.Setup(settings);
			speed = settings["speed"].Number(1);
		}

		public static void RenderDebug(JsonValue root) {
			var speed = root["speed"].Number(1);

			if (ImGui.InputFloat("Speed", ref speed)) {
				root["speed"] = speed;
			}
		}
	}