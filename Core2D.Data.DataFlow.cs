    [DataContract(IsReference = true)]
    public class DataFlow
    {
        public void Bind(ProjectContainer project)
        {
            foreach (var document in project.Documents)
            {
                Bind(document);
            }
        }

        public void Bind(DocumentContainer document)
        {
            foreach (var container in document.Pages)
            {
                var db = container.Properties;
                var r = container.Record;

                Bind(container.Template, db, r);
                Bind(container, db, r);
            }
        }

        public void Bind(PageContainer container, object db, object r)
        {
            foreach (var layer in container.Layers)
            {
                Bind(layer, db, r);
            }
        }

        public void Bind(LayerContainer layer, object db, object r)
        {
            foreach (var shape in layer.Shapes)
            {
                shape.Bind(this, db, r);
            }
        }

        public void Bind(LineShape line, object db, object r)
        {
        }

        public void Bind(RectangleShape rectangle, object db, object r)
        {
        }

        public void Bind(EllipseShape ellipse, object db, object r)
        {
        }

        public void Bind(ArcShape arc, object db, object r)
        {
        }

        public void Bind(CubicBezierShape cubicBezier, object db, object r)
        {
        }

        public void Bind(QuadraticBezierShape quadraticBezier, object db, object r)
        {
        }

        public void Bind(TextShape text, object db, object r)
        {
            var properties = (ImmutableArray<Property>)db;
            var record = (Record)r;
            var tbind = TextBinding.Bind(text, properties, record);
            text.SetProperty(nameof(TextShape.Text), tbind);
        }

        public void Bind(ImageShape image, object db, object r)
        {
        }

        public void Bind(PathShape path, object db, object r)
        {
        }
    }