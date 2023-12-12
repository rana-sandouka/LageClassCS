	public class CastleBiome : Biome {
		public CastleBiome() : base("Born to do rogueries", Biome.Castle, "castle_biome", new Color(14, 7, 27)) {
		
		}

		public override string GetMusic() {
			if (Run.AlternateMusic) {
				return "chip";
			}

			return base.GetMusic();
		}

		public override void ModifyPainter(Level level, Painter painter) {
			base.ModifyPainter(level, painter);
			
			painter.Modifiers.Add((l, rm, x, y) => {
				if (rm is TrapRoom) {
					return;
				}
				
				var f = Run.Depth == 1;
				
				var r = (byte) (f ? Tiles.RandomFloor() : Tile.Chasm);
				
				if (l.Get(x, y, true) == Tile.Lava || (!(rm is TreasureRoom) && f && l.Get(x, y) == Tile.Chasm)) {
					var i = l.ToIndex(x, y);
					
					l.Liquid[i] = 0;
					l.Tiles[i] = r;
				}
			});
		}

		public override Tile GetFilling() {
			return Run.Level.Variant.Id == LevelVariant.Gold ? Tile.WallB : base.GetFilling();
		}

		public override string GetStepSound(Tile tile) {
			if (tile == Tile.FloorB) {
				return $"player_step_wood_{Rnd.Int(1, 4)}";
			}
			
			return base.GetStepSound(tile);
		}

		public override int GetNumRegularRooms() {
			return (int) (base.GetNumRegularRooms() * 2f);
		}
	}