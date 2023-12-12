    public class SpinnerPiece : BlueprintPiece<Spinner>
    {
        private readonly CircularContainer circle;
        private readonly RingPiece ring;

        public SpinnerPiece()
        {
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;
            FillMode = FillMode.Fit;
            Size = new Vector2(1.3f);

            InternalChildren = new Drawable[]
            {
                circle = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Alpha = 0.5f,
                    Child = new Box { RelativeSizeAxes = Axes.Both }
                },
                ring = new RingPiece()
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }

        public override void UpdateFrom(Spinner hitObject)
        {
            base.UpdateFrom(hitObject);

            ring.Scale = new Vector2(hitObject.Scale);
        }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => circle.ReceivePositionalInputAt(screenSpacePos);
    }