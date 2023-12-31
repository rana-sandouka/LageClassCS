    public class EffectControlPoint : ControlPoint
    {
        public static readonly EffectControlPoint DEFAULT = new EffectControlPoint
        {
            KiaiModeBindable = { Disabled = true },
            OmitFirstBarLineBindable = { Disabled = true }
        };

        /// <summary>
        /// Whether the first bar line of this control point is ignored.
        /// </summary>
        public readonly BindableBool OmitFirstBarLineBindable = new BindableBool();

        public override Color4 GetRepresentingColour(OsuColour colours) => colours.Purple;

        /// <summary>
        /// Whether the first bar line of this control point is ignored.
        /// </summary>
        public bool OmitFirstBarLine
        {
            get => OmitFirstBarLineBindable.Value;
            set => OmitFirstBarLineBindable.Value = value;
        }

        /// <summary>
        /// Whether this control point enables Kiai mode.
        /// </summary>
        public readonly BindableBool KiaiModeBindable = new BindableBool();

        /// <summary>
        /// Whether this control point enables Kiai mode.
        /// </summary>
        public bool KiaiMode
        {
            get => KiaiModeBindable.Value;
            set => KiaiModeBindable.Value = value;
        }

        public override bool IsRedundant(ControlPoint existing)
            => !OmitFirstBarLine
               && existing is EffectControlPoint existingEffect
               && KiaiMode == existingEffect.KiaiMode
               && OmitFirstBarLine == existingEffect.OmitFirstBarLine;

        public override void CopyFrom(ControlPoint other)
        {
            KiaiMode = ((EffectControlPoint)other).KiaiMode;
            OmitFirstBarLine = ((EffectControlPoint)other).OmitFirstBarLine;

            base.CopyFrom(other);
        }
    }