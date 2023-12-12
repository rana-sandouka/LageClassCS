    public abstract class BaseTool : BaseShape
    {
        public override ShapeCategory ShapeCategory { get; } = ShapeCategory.Tool;

        public virtual void OnDraw(Graphics g)
        {
        }
    }