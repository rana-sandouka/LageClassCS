    public class PlayBeatmapDetailArea : BeatmapDetailArea
    {
        public readonly BeatmapLeaderboard Leaderboard;

        public override WorkingBeatmap Beatmap
        {
            get => base.Beatmap;
            set
            {
                base.Beatmap = value;

                Leaderboard.Beatmap = value is DummyWorkingBeatmap ? null : value?.BeatmapInfo;
            }
        }

        private Bindable<TabType> selectedTab;

        private Bindable<bool> selectedModsFilter;

        public PlayBeatmapDetailArea()
        {
            Add(Leaderboard = new BeatmapLeaderboard { RelativeSizeAxes = Axes.Both });
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            selectedTab = config.GetBindable<TabType>(OsuSetting.BeatmapDetailTab);
            selectedModsFilter = config.GetBindable<bool>(OsuSetting.BeatmapDetailModsFilter);

            selectedTab.BindValueChanged(tab => CurrentTab.Value = getTabItemFromTabType(tab.NewValue), true);
            CurrentTab.BindValueChanged(tab => selectedTab.Value = getTabTypeFromTabItem(tab.NewValue));

            selectedModsFilter.BindValueChanged(checkbox => CurrentModsFilter.Value = checkbox.NewValue, true);
            CurrentModsFilter.BindValueChanged(checkbox => selectedModsFilter.Value = checkbox.NewValue);
        }

        public override void Refresh()
        {
            base.Refresh();

            Leaderboard.RefreshScores();
        }

        protected override void OnTabChanged(BeatmapDetailAreaTabItem tab, bool selectedMods)
        {
            base.OnTabChanged(tab, selectedMods);

            Leaderboard.FilterMods = selectedMods;

            switch (tab)
            {
                case BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope> leaderboard:
                    Leaderboard.Scope = leaderboard.Scope;
                    Leaderboard.Show();
                    break;

                default:
                    Leaderboard.Hide();
                    break;
            }
        }

        protected override BeatmapDetailAreaTabItem[] CreateTabItems() => base.CreateTabItems().Concat(new BeatmapDetailAreaTabItem[]
        {
            new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Local),
            new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Country),
            new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Global),
            new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Friend),
        }).ToArray();

        private BeatmapDetailAreaTabItem getTabItemFromTabType(TabType type)
        {
            switch (type)
            {
                case TabType.Details:
                    return new BeatmapDetailAreaDetailTabItem();

                case TabType.Local:
                    return new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Local);

                case TabType.Country:
                    return new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Country);

                case TabType.Global:
                    return new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Global);

                case TabType.Friends:
                    return new BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope>(BeatmapLeaderboardScope.Friend);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        private TabType getTabTypeFromTabItem(BeatmapDetailAreaTabItem item)
        {
            switch (item)
            {
                case BeatmapDetailAreaDetailTabItem _:
                    return TabType.Details;

                case BeatmapDetailAreaLeaderboardTabItem<BeatmapLeaderboardScope> leaderboardTab:
                    switch (leaderboardTab.Scope)
                    {
                        case BeatmapLeaderboardScope.Local:
                            return TabType.Local;

                        case BeatmapLeaderboardScope.Country:
                            return TabType.Country;

                        case BeatmapLeaderboardScope.Global:
                            return TabType.Global;

                        case BeatmapLeaderboardScope.Friend:
                            return TabType.Friends;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(item));
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(item));
            }
        }

        public enum TabType
        {
            Details,
            Local,
            Country,
            Global,
            Friends
        }
    }