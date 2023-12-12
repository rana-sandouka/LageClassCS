    internal class QuadraticBezierDrawNode : DrawNode, IQuadraticBezierDrawNode
    {
        public QuadraticBezierShape QuadraticBezier { get; set; }
        public AM.Geometry Geometry { get; set; }

        public QuadraticBezierDrawNode(QuadraticBezierShape quadraticBezier, ShapeStyle style)
        {
            Style = style;
            QuadraticBezier = quadraticBezier;
            UpdateGeometry();
        }

        public override void UpdateGeometry()
        {
            ScaleThickness = QuadraticBezier.State.Flags.HasFlag(ShapeStateFlags.Thickness);
            ScaleSize = QuadraticBezier.State.Flags.HasFlag(ShapeStateFlags.Size);
            Geometry = PathGeometryConverter.ToGeometry(QuadraticBezier);
            Center = Geometry.Bounds.Center;
        }

        public override void OnDraw(object dc, double zoom)
        {
            var context = dc as AM.DrawingContext;

            context.DrawGeometry(QuadraticBezier.IsFilled ? Fill : null, QuadraticBezier.IsStroked ? Stroke : null, Geometry);
        }
    }