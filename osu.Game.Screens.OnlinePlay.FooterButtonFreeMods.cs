    public class FooterButtonFreeMods : FooterButton, IHasCurrentValue<IReadOnlyList<Mod>>
    {
        public Bindable<IReadOnlyList<Mod>> Current
        {
            get => modDisplay.Current;
            set => modDisplay.Current = value;
        }

        private readonly ModDisplay modDisplay;

        public FooterButtonFreeMods()
        {
            ButtonContentContainer.Add(modDisplay = new ModDisplay
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                DisplayUnrankedText = false,
                Scale = new Vector2(0.8f),
                ExpansionMode = ExpansionMode.AlwaysContracted,
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            SelectedColour = colours.Yellow;
            DeselectedColour = SelectedColour.Opacity(0.5f);
            Text = @"freemods";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Current.BindValueChanged(_ => updateModDisplay(), true);
        }

        private void updateModDisplay()
        {
            if (Current.Value?.Count > 0)
                modDisplay.FadeIn();
            else
                modDisplay.FadeOut();
        }
    }