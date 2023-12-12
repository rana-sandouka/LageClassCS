    public class CatchComboDisplay : SkinnableDrawable
    {
        private int currentCombo;

        [CanBeNull]
        public ICatchComboCounter ComboCounter => Drawable as ICatchComboCounter;

        public CatchComboDisplay()
            : base(new CatchSkinComponent(CatchSkinComponents.CatchComboCounter), _ => Empty())
        {
        }

        protected override void SkinChanged(ISkinSource skin, bool allowFallback)
        {
            base.SkinChanged(skin, allowFallback);
            ComboCounter?.UpdateCombo(currentCombo);
        }

        public void OnNewResult(DrawableCatchHitObject judgedObject, JudgementResult result)
        {
            if (!result.Type.AffectsCombo() || !result.HasResult)
                return;

            if (!result.IsHit)
            {
                updateCombo(0, null);
                return;
            }

            updateCombo(result.ComboAtJudgement + 1, judgedObject.AccentColour.Value);
        }

        public void OnRevertResult(DrawableCatchHitObject judgedObject, JudgementResult result)
        {
            if (!result.Type.AffectsCombo() || !result.HasResult)
                return;

            updateCombo(result.ComboAtJudgement, judgedObject.AccentColour.Value);
        }

        private void updateCombo(int newCombo, Color4? hitObjectColour)
        {
            currentCombo = newCombo;
            ComboCounter?.UpdateCombo(newCombo, hitObjectColour);
        }
    }