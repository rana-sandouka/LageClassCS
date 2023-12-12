        private class TaikoHalfDrum : Container, IKeyBindingHandler<TaikoAction>
        {
            /// <summary>
            /// The key to be used for the rim of the half-drum.
            /// </summary>
            public TaikoAction RimAction;

            /// <summary>
            /// The key to be used for the centre of the half-drum.
            /// </summary>
            public TaikoAction CentreAction;

            private readonly Sprite rim;
            private readonly Sprite rimHit;
            private readonly Sprite centre;
            private readonly Sprite centreHit;

            [Resolved]
            private DrumSampleContainer sampleContainer { get; set; }

            public TaikoHalfDrum(bool flipped)
            {
                Masking = true;

                Children = new Drawable[]
                {
                    rim = new Sprite
                    {
                        Anchor = flipped ? Anchor.CentreLeft : Anchor.CentreRight,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both
                    },
                    rimHit = new Sprite
                    {
                        Anchor = flipped ? Anchor.CentreLeft : Anchor.CentreRight,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0,
                        Blending = BlendingParameters.Additive,
                    },
                    centre = new Sprite
                    {
                        Anchor = flipped ? Anchor.CentreLeft : Anchor.CentreRight,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.7f)
                    },
                    centreHit = new Sprite
                    {
                        Anchor = flipped ? Anchor.CentreLeft : Anchor.CentreRight,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.7f),
                        Alpha = 0,
                        Blending = BlendingParameters.Additive
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures, OsuColour colours)
            {
                rim.Texture = textures.Get(@"Gameplay/taiko/taiko-drum-outer");
                rimHit.Texture = textures.Get(@"Gameplay/taiko/taiko-drum-outer-hit");
                centre.Texture = textures.Get(@"Gameplay/taiko/taiko-drum-inner");
                centreHit.Texture = textures.Get(@"Gameplay/taiko/taiko-drum-inner-hit");

                rimHit.Colour = colours.Blue;
                centreHit.Colour = colours.Pink;
            }

            [Resolved(canBeNull: true)]
            private GameplayClock gameplayClock { get; set; }

            public bool OnPressed(TaikoAction action)
            {
                Drawable target = null;
                Drawable back = null;

                var drumSample = sampleContainer.SampleAt(Time.Current);

                if (action == CentreAction)
                {
                    target = centreHit;
                    back = centre;

                    drumSample.Centre?.Play();
                }
                else if (action == RimAction)
                {
                    target = rimHit;
                    back = rim;

                    drumSample.Rim?.Play();
                }

                if (target != null)
                {
                    const float scale_amount = 0.05f;
                    const float alpha_amount = 0.5f;

                    const float down_time = 40;
                    const float up_time = 1000;

                    back.ScaleTo(target.Scale.X - scale_amount, down_time, Easing.OutQuint)
                        .Then()
                        .ScaleTo(1, up_time, Easing.OutQuint);

                    target.Animate(
                        t => t.ScaleTo(target.Scale.X - scale_amount, down_time, Easing.OutQuint),
                        t => t.FadeTo(Math.Min(target.Alpha + alpha_amount, 1), down_time, Easing.OutQuint)
                    ).Then(
                        t => t.ScaleTo(1, up_time, Easing.OutQuint),
                        t => t.FadeOut(up_time, Easing.OutQuint)
                    );
                }

                return false;
            }

            public void OnReleased(TaikoAction action)
            {
            }
        }