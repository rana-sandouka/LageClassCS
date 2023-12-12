    public class OsuModSpinIn : ModWithVisibilityAdjustment
    {
        public override string Name => "Spin In";
        public override string Acronym => "SI";
        public override IconUsage? Icon => FontAwesome.Solid.Undo;
        public override ModType Type => ModType.Fun;
        public override string Description => "Circles spin in. No approach circles.";
        public override double ScoreMultiplier => 1;

        // todo: this mod should be able to be compatible with hidden with a bit of further implementation.
        public override Type[] IncompatibleMods => new[] { typeof(OsuModObjectScaleTween), typeof(OsuModHidden), typeof(OsuModTraceable) };

        private const int rotate_offset = 360;
        private const float rotate_starting_width = 2;

        protected override void ApplyIncreasedVisibilityState(DrawableHitObject hitObject, ArmedState state)
        {
        }

        protected override void ApplyNormalVisibilityState(DrawableHitObject hitObject, ArmedState state) => applyZoomState(hitObject, state);

        private void applyZoomState(DrawableHitObject drawable, ArmedState state)
        {
            if (drawable is DrawableSpinner)
                return;

            var h = (OsuHitObject)drawable.HitObject;

            switch (drawable)
            {
                case DrawableHitCircle circle:
                    using (circle.BeginAbsoluteSequence(h.StartTime - h.TimePreempt, true))
                    {
                        circle.ApproachCircle.Hide();

                        circle.RotateTo(rotate_offset).Then().RotateTo(0, h.TimePreempt, Easing.InOutSine);
                        circle.ScaleTo(new Vector2(rotate_starting_width, 0)).Then().ScaleTo(1, h.TimePreempt, Easing.InOutSine);

                        // bypass fade in.
                        if (state == ArmedState.Idle)
                            circle.FadeIn();
                    }

                    break;

                case DrawableSlider slider:
                    using (slider.BeginAbsoluteSequence(h.StartTime - h.TimePreempt))
                    {
                        slider.ScaleTo(0).Then().ScaleTo(1, h.TimePreempt, Easing.InOutSine);

                        // bypass fade in.
                        if (state == ArmedState.Idle)
                            slider.FadeIn();
                    }

                    break;
            }
        }
    }