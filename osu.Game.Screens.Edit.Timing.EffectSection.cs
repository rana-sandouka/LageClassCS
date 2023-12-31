    internal class EffectSection : Section<EffectControlPoint>
    {
        private LabelledSwitchButton kiai;
        private LabelledSwitchButton omitBarLine;

        [BackgroundDependencyLoader]
        private void load()
        {
            Flow.AddRange(new[]
            {
                kiai = new LabelledSwitchButton { Label = "Kiai Time" },
                omitBarLine = new LabelledSwitchButton { Label = "Skip Bar Line" },
            });
        }

        protected override void OnControlPointChanged(ValueChangedEvent<EffectControlPoint> point)
        {
            if (point.NewValue != null)
            {
                kiai.Current = point.NewValue.KiaiModeBindable;
                kiai.Current.BindValueChanged(_ => ChangeHandler?.SaveState());

                omitBarLine.Current = point.NewValue.OmitFirstBarLineBindable;
                omitBarLine.Current.BindValueChanged(_ => ChangeHandler?.SaveState());
            }
        }

        protected override EffectControlPoint CreatePoint()
        {
            var reference = Beatmap.ControlPointInfo.EffectPointAt(SelectedGroup.Value.Time);

            return new EffectControlPoint
            {
                KiaiMode = reference.KiaiMode,
                OmitFirstBarLine = reference.OmitFirstBarLine
            };
        }
    }