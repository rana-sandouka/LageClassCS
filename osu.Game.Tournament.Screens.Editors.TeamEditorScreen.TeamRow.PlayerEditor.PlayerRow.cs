                public class PlayerRow : CompositeDrawable
                {
                    private readonly User user;

                    [Resolved]
                    protected IAPIProvider API { get; private set; }

                    [Resolved]
                    private TournamentGameBase game { get; set; }

                    private readonly Bindable<string> userId = new Bindable<string>();

                    private readonly Container drawableContainer;

                    public PlayerRow(TournamentTeam team, User user)
                    {
                        this.user = user;

                        Margin = new MarginPadding(10);

                        RelativeSizeAxes = Axes.X;
                        AutoSizeAxes = Axes.Y;

                        Masking = true;
                        CornerRadius = 5;

                        InternalChildren = new Drawable[]
                        {
                            new Box
                            {
                                Colour = OsuColour.Gray(0.2f),
                                RelativeSizeAxes = Axes.Both,
                            },
                            new FillFlowContainer
                            {
                                Margin = new MarginPadding(5),
                                Padding = new MarginPadding { Right = 160 },
                                Spacing = new Vector2(5),
                                Direction = FillDirection.Horizontal,
                                AutoSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new SettingsNumberBox
                                    {
                                        LabelText = "User ID",
                                        RelativeSizeAxes = Axes.None,
                                        Width = 200,
                                        Current = userId,
                                    },
                                    drawableContainer = new Container
                                    {
                                        Size = new Vector2(100, 70),
                                    },
                                }
                            },
                            new DangerousSettingsButton
                            {
                                Anchor = Anchor.CentreRight,
                                Origin = Anchor.CentreRight,
                                RelativeSizeAxes = Axes.None,
                                Width = 150,
                                Text = "Delete Player",
                                Action = () =>
                                {
                                    Expire();
                                    team.Players.Remove(user);
                                },
                            }
                        };
                    }

                    [BackgroundDependencyLoader]
                    private void load()
                    {
                        userId.Value = user.Id.ToString();
                        userId.BindValueChanged(idString =>
                        {
                            int.TryParse(idString.NewValue, out var parsed);

                            user.Id = parsed;

                            if (idString.NewValue != idString.OldValue)
                                user.Username = string.Empty;

                            if (!string.IsNullOrEmpty(user.Username))
                            {
                                updatePanel();
                                return;
                            }

                            game.PopulateUser(user, updatePanel, updatePanel);
                        }, true);
                    }

                    private void updatePanel()
                    {
                        drawableContainer.Child = new UserGridPanel(user) { Width = 300 };
                    }
                }