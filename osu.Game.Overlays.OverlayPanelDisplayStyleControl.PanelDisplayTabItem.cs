        private class PanelDisplayTabItem : TabItem<OverlayPanelDisplayStyle>, IHasTooltip
        {
            public IconUsage Icon
            {
                set => icon.Icon = value;
            }

            [Resolved]
            private OverlayColourProvider colourProvider { get; set; }

            public string TooltipText => $@"{Value} view";

            private readonly SpriteIcon icon;

            public PanelDisplayTabItem(OverlayPanelDisplayStyle value)
                : base(value)
            {
                Size = new Vector2(11);
                AddRange(new Drawable[]
                {
                    icon = new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit
                    },
                    new HoverClickSounds()
                });
            }

            protected override void OnActivated() => updateState();

            protected override void OnDeactivated() => updateState();

            protected override bool OnHover(HoverEvent e)
            {
                updateState();
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                updateState();
                base.OnHoverLost(e);
            }

            private void updateState() => icon.Colour = Active.Value || IsHovered ? colourProvider.Light1 : Color4.White;
        }