    public class ChevronButton : OsuHoverContainer
    {
        public readonly BindableBool Expanded = new BindableBool(true);

        private readonly SpriteIcon icon;

        public ChevronButton()
        {
            Size = new Vector2(40, 22);
            Child = icon = new SpriteIcon
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(12),
            };
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            IdleColour = HoverColour = colourProvider.Foreground1;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Action = Expanded.Toggle;
            Expanded.BindValueChanged(onExpandedChanged, true);
        }

        private void onExpandedChanged(ValueChangedEvent<bool> expanded)
        {
            icon.Icon = expanded.NewValue ? FontAwesome.Solid.ChevronUp : FontAwesome.Solid.ChevronDown;
        }
    }