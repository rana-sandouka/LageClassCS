		public class DuckState : EntityState {
			public override void Init() {
				base.Init();
				Quacks++;
				if (Quacks >= 150) {
					Achievements.Unlock("bk:quackers");
				}

				var hat = Self.GetComponent<HatComponent>().Item;

				if (hat != null && (hat.Id == "bk:villager_head" || hat.Id == "bk:stone_hat")) {
					Audio.PlaySfx($"villager{Rnd.Int(1, 5)}", 1f, Rnd.Float(-0.5f, 0.5f));
				} else {
					Audio.PlaySfx("quck", 1f, Rnd.Float(-0.5f, 0.5f));
				}
				
				Self.HandleEvent(new QuackEvent {
					Player = (Player) Self
				});
			}

			public override void Update(float dt) {
				base.Update(dt);

				if (Self.GetComponent<RectBodyComponent>().Velocity.Length() > 10f) {
					if (T >= 0.2f) {
						AnimationUtil.Poof(Self.Center);
						T = 0;
					}
				} else {
					T = 0;
				}
			}
		}