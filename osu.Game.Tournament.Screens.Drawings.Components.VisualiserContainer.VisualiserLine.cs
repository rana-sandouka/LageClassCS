        private class VisualiserLine : Container
        {
            /// <summary>
            /// Time offset.
            /// </summary>
            public float Offset;

            public double CycleTime;

            private float leftPos => -(float)((Time.Current + Offset) / CycleTime) + expiredCount;

            private Texture texture;

            private int expiredCount;

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                texture = textures.Get("Drawings/visualiser-line");
            }

            protected override void UpdateAfterChildren()
            {
                base.UpdateAfterChildren();

                while (Children.Count < 3)
                    addLine();

                float pos = leftPos;

                foreach (var c in Children)
                {
                    if (c.Position.X < -1)
                    {
                        c.ClearTransforms();
                        c.Expire();
                        expiredCount++;
                    }
                    else
                        c.MoveToX(pos, 100);

                    pos += 1;
                }
            }

            private void addLine()
            {
                Add(new Sprite
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,

                    RelativePositionAxes = Axes.Both,
                    RelativeSizeAxes = Axes.Both,

                    Texture = texture,

                    X = leftPos + 1
                });
            }
        }