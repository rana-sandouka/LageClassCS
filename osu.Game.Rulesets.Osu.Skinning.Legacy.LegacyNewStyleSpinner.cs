    public class LegacyNewStyleSpinner : LegacySpinner
    {
        private Sprite glow;
        private Sprite discBottom;
        private Sprite discTop;
        private Sprite spinningMiddle;
        private Sprite fixedMiddle;

        private readonly Color4 glowColour = new Color4(3, 151, 255, 255);

        private Container scaleContainer;

        [BackgroundDependencyLoader]
        private void load(ISkinSource source)
        {
            AddInternal(scaleContainer = new Container
            {
                Scale = new Vector2(SPRITE_SCALE),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    glow = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = source.GetTexture("spinner-glow"),
                        Blending = BlendingParameters.Additive,
                        Colour = glowColour,
                    },
                    discBottom = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = source.GetTexture("spinner-bottom")
                    },
                    discTop = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = source.GetTexture("spinner-top")
                    },
                    fixedMiddle = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = source.GetTexture("spinner-middle")
                    },
                    spinningMiddle = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = source.GetTexture("spinner-middle2")
                    }
                }
            });
        }

        protected override void UpdateStateTransforms(DrawableHitObject drawableHitObject, ArmedState state)
        {
            base.UpdateStateTransforms(drawableHitObject, state);

            switch (drawableHitObject)
            {
                case DrawableSpinner d:
                    Spinner spinner = d.HitObject;

                    using (BeginAbsoluteSequence(spinner.StartTime - spinner.TimePreempt, true))
                        this.FadeOut();

                    using (BeginAbsoluteSequence(spinner.StartTime - spinner.TimeFadeIn / 2, true))
                        this.FadeInFromZero(spinner.TimeFadeIn / 2);

                    using (BeginAbsoluteSequence(spinner.StartTime - spinner.TimePreempt, true))
                    {
                        fixedMiddle.FadeColour(Color4.White);

                        using (BeginDelayedSequence(spinner.TimePreempt, true))
                            fixedMiddle.FadeColour(Color4.Red, spinner.Duration);
                    }

                    if (state == ArmedState.Hit)
                    {
                        using (BeginAbsoluteSequence(d.HitStateUpdateTime))
                            glow.FadeOut(300);
                    }

                    break;

                case DrawableSpinnerBonusTick _:
                    if (state == ArmedState.Hit)
                        glow.FlashColour(Color4.White, 200);

                    break;
            }
        }

        protected override void Update()
        {
            base.Update();
            spinningMiddle.Rotation = discTop.Rotation = DrawableSpinner.RotationTracker.Rotation;
            discBottom.Rotation = discTop.Rotation / 3;

            glow.Alpha = DrawableSpinner.Progress;

            scaleContainer.Scale = new Vector2(SPRITE_SCALE * (0.8f + (float)Interpolation.ApplyEasing(Easing.Out, DrawableSpinner.Progress) * 0.2f));
        }
    }