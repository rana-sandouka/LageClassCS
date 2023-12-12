    [DataContract(IsReference = true)]
    public class CubicBezierSegment : PathSegment
    {
        private PointShape _point1;
        private PointShape _point2;
        private PointShape _point3;

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public PointShape Point1
        {
            get => _point1;
            set => RaiseAndSetIfChanged(ref _point1, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public PointShape Point2
        {
            get => _point2;
            set => RaiseAndSetIfChanged(ref _point2, value);
        }

        [DataMember(IsRequired = false, EmitDefaultValue = true)]
        public PointShape Point3
        {
            get => _point3;
            set => RaiseAndSetIfChanged(ref _point3, value);
        }

        public override void GetPoints(IList<PointShape> points)
        {
            points.Add(Point1);
            points.Add(Point2);
            points.Add(Point3);
        }

        public override object Copy(IDictionary<object, object> shared)
        {
            throw new NotImplementedException();
        }

        public override bool IsDirty()
        {
            var isDirty = base.IsDirty();

            isDirty |= Point1.IsDirty();
            isDirty |= Point2.IsDirty();
            isDirty |= Point3.IsDirty();

            return isDirty;
        }

        public override void Invalidate()
        {
            base.Invalidate();

            Point1.Invalidate();
            Point2.Invalidate();
            Point3.Invalidate();
        }

        public override string ToXamlString()
            => $"C{Point1.ToXamlString()} {Point2.ToXamlString()} {Point3.ToXamlString()}";

        public override string ToSvgString()
            => $"C{Point1.ToSvgString()} {Point2.ToSvgString()} {Point3.ToSvgString()}";
    }