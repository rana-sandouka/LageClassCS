        private class CloseButton : OsuClickableContainer
        {
            private Color4 hoverColour;

            public CloseButton()
            {
                Colour = OsuColour.Gray(0.2f);
                AutoSizeAxes = Axes.Both;

                Children = new[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.Solid.TimesCircle,
                        Size = new Vector2(20),
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                hoverColour = colours.Yellow;
            }

            protected override bool OnHover(HoverEvent e)
            {
                this.FadeColour(hoverColour, 200);
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                this.FadeColour(OsuColour.Gray(0.2f), 200);
                base.OnHoverLost(e);
            }
        }