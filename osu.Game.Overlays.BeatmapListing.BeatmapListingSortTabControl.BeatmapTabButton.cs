        private class BeatmapTabButton : TabButton
        {
            public readonly Bindable<SortDirection> SortDirection = new Bindable<SortDirection>();

            protected override Color4 ContentColour
            {
                set
                {
                    base.ContentColour = value;
                    icon.Colour = value;
                }
            }

            private readonly SpriteIcon icon;

            public BeatmapTabButton(SortCriteria value)
                : base(value)
            {
                Add(icon = new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AlwaysPresent = true,
                    Alpha = 0,
                    Size = new Vector2(6)
                });
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                SortDirection.BindValueChanged(direction =>
                {
                    icon.Icon = direction.NewValue == Overlays.SortDirection.Ascending ? FontAwesome.Solid.CaretUp : FontAwesome.Solid.CaretDown;
                }, true);
            }

            protected override void UpdateState()
            {
                base.UpdateState();
                icon.FadeTo(Active.Value || IsHovered ? 1 : 0, 200, Easing.OutQuint);
            }

            protected override bool OnClick(ClickEvent e)
            {
                if (Active.Value)
                    SortDirection.Value = SortDirection.Value == Overlays.SortDirection.Ascending ? Overlays.SortDirection.Descending : Overlays.SortDirection.Ascending;

                return base.OnClick(e);
            }
        }