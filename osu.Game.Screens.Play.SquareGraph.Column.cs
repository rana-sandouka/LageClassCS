        public class Column : Container, IStateful<ColumnState>
        {
            protected readonly Color4 EmptyColour = Color4.White.Opacity(20);
            public Color4 LitColour = Color4.LightBlue;
            protected readonly Color4 DimmedColour = Color4.White.Opacity(140);

            private float cubeCount => DrawHeight / WIDTH;
            private const float cube_size = 4;
            private const float padding = 2;
            public const float WIDTH = cube_size + padding;

            public event Action<ColumnState> StateChanged;

            private readonly List<Box> drawableRows = new List<Box>();

            private float filled;

            public float Filled
            {
                get => filled;
                set
                {
                    if (value == filled) return;

                    filled = value;
                    fillActive();
                }
            }

            private ColumnState state;

            public ColumnState State
            {
                get => state;
                set
                {
                    if (value == state) return;

                    state = value;
                    if (IsLoaded)
                        fillActive();

                    StateChanged?.Invoke(State);
                }
            }

            public Column(float height)
            {
                Width = WIDTH;
                Height = height;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                drawableRows.AddRange(Enumerable.Range(0, (int)cubeCount).Select(r => new Box
                {
                    Size = new Vector2(cube_size),
                    Position = new Vector2(0, r * WIDTH + padding),
                }));

                Children = drawableRows;

                // Reverse drawableRows so when iterating through them they start at the bottom
                drawableRows.Reverse();
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                fillActive();
            }

            private void fillActive()
            {
                Color4 colour = State == ColumnState.Lit ? LitColour : DimmedColour;

                int countFilled = (int)Math.Clamp(filled * drawableRows.Count, 0, drawableRows.Count);

                for (int i = 0; i < drawableRows.Count; i++)
                    drawableRows[i].Colour = i < countFilled ? colour : EmptyColour;
            }
        }