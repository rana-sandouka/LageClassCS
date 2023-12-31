    public class CollectionDropdown : CollectionFilterDropdown
    {
        protected override bool ShowManageCollectionsItem => false;

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AccentColour = colours.Gray6;
        }

        protected override CollectionDropdownHeader CreateCollectionHeader() => new CollectionsHeader();

        protected override CollectionDropdownMenu CreateCollectionMenu() => new CollectionsMenu();

        private class CollectionsMenu : CollectionDropdownMenu
        {
            public CollectionsMenu()
            {
                Masking = true;
                CornerRadius = 5;
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                BackgroundColour = colours.Gray4;
            }
        }

        private class CollectionsHeader : CollectionDropdownHeader
        {
            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                BackgroundColour = colours.Gray4;
            }

            public CollectionsHeader()
            {
                CornerRadius = 5;
                Height = 30;
                Icon.Size = new Vector2(14);
                Icon.Margin = new MarginPadding(0);
                Foreground.Padding = new MarginPadding { Top = 4, Bottom = 4, Left = 10, Right = 10 };
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = Color4.Black.Opacity(0.3f),
                    Radius = 3,
                    Offset = new Vector2(0f, 1f),
                };
            }
        }
    }