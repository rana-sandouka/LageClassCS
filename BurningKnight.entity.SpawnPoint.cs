	public class SpawnPoint : SaveableEntity, PlaceableEntity {
		public override void AddComponents() {
			base.AddComponents();
			AddTag(Tags.Checkpoint);
		}

		public override void Render() {
			if (Engine.EditingLevel) {
				Graphics.Batch.FillRectangle(X, Y, Width, Height, ColorUtils.WhiteColor);
			}
		}
	}