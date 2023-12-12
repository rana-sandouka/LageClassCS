	public class TargetLineNode
	{
		public readonly Target Target;
		public readonly Color Color;
		public readonly Sprite Tile;

		public TargetLineNode(in Target target, Color color, Sprite tile = null)
		{
			// Note: Not all activities are drawable. In that case, pass Target.Invalid as target,
			// if "yield break" in TargetLineNode(Actor self) is not feasible.
			Target = target;
			Color = color;
			Tile = tile;
		}
	}