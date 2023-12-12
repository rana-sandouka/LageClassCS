    public class ProfileHeader : TabControlOverlayHeader<string>
    {
        private UserCoverBackground coverContainer;

        public Bindable<User> User = new Bindable<User>();

        private CentreHeaderContainer centreHeaderContainer;
        private DetailHeaderContainer detailHeaderContainer;

        public ProfileHeader()
        {
            ContentSidePadding = UserProfileOverlay.CONTENT_X_MARGIN;

            User.ValueChanged += e => updateDisplay(e.NewValue);

            TabControl.AddItem("info");
            TabControl.AddItem("modding");

            centreHeaderContainer.DetailsVisible.BindValueChanged(visible => detailHeaderContainer.Expanded = visible.NewValue, true);
        }

        protected override Drawable CreateBackground() =>
            new Container
            {
                RelativeSizeAxes = Axes.X,
                Height = 150,
                Masking = true,
                Children = new Drawable[]
                {
                    coverContainer = new ProfileCoverBackground
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ColourInfo.GradientVertical(Color4Extensions.FromHex("222").Opacity(0.8f), Color4Extensions.FromHex("222").Opacity(0.2f))
                    },
                }
            };

        protected override Drawable CreateContent() => new FillFlowContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            Direction = FillDirection.Vertical,
            Children = new Drawable[]
            {
                new TopHeaderContainer
                {
                    RelativeSizeAxes = Axes.X,
                    User = { BindTarget = User },
                },
                centreHeaderContainer = new CentreHeaderContainer
                {
                    RelativeSizeAxes = Axes.X,
                    User = { BindTarget = User },
                },
                detailHeaderContainer = new DetailHeaderContainer
                {
                    RelativeSizeAxes = Axes.X,
                    User = { BindTarget = User },
                },
                new MedalHeaderContainer
                {
                    RelativeSizeAxes = Axes.X,
                    User = { BindTarget = User },
                },
                new BottomHeaderContainer
                {
                    RelativeSizeAxes = Axes.X,
                    User = { BindTarget = User },
                },
            }
        };

        protected override OverlayTitle CreateTitle() => new ProfileHeaderTitle();

        private void updateDisplay(User user) => coverContainer.User = user;

        private class ProfileHeaderTitle : OverlayTitle
        {
            public ProfileHeaderTitle()
            {
                Title = "player info";
                IconTexture = "Icons/Hexacons/profile";
            }
        }

        private class ProfileCoverBackground : UserCoverBackground
        {
            protected override double LoadDelay => 0;
        }
    }