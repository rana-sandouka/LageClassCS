    public class GitHubUpdateManager : IDisposable
    {
        public bool AutoUpdateEnabled { get; set; } // ConfigureAutoUpdate function must be called after change this
        public TimeSpan UpdateCheckInterval { get; private set; } = TimeSpan.FromHours(1);
        public TimeSpan UpdateReCheckInterval { get; private set; } = TimeSpan.FromHours(24); // If "No" button pressed in update message box then this interval will be used
        public string GitHubOwner { get; set; }
        public string GitHubRepo { get; set; }
        public bool IsBeta { get; set; } // If current build is beta and latest stable release is same version as current build then it will be downloaded
        public bool IsPortable { get; set; } // If current build is portable then download URL will be opened in browser instead of downloading it
        public bool CheckPreReleaseUpdates { get; set; }

        private bool firstUpdateCheck = true;
        private Timer updateTimer = null;
        private readonly object updateTimerLock = new object();

        public GitHubUpdateManager(string owner, string repo)
        {
            GitHubOwner = owner;
            GitHubRepo = repo;
        }

        public GitHubUpdateManager(string owner, string repo, bool beta, bool portable) : this(owner, repo)
        {
            IsBeta = beta;
            IsPortable = portable;
        }

        public void ConfigureAutoUpdate()
        {
            lock (updateTimerLock)
            {
                if (AutoUpdateEnabled)
                {
                    if (updateTimer == null)
                    {
                        updateTimer = new Timer(state => CheckUpdate(), null, TimeSpan.Zero, UpdateCheckInterval);
                    }
                }
                else
                {
                    Dispose();
                }
            }
        }

        private void CheckUpdate()
        {
            if (!UpdateMessageBox.DontShow && !UpdateMessageBox.IsOpen)
            {
                UpdateChecker updateChecker = CreateUpdateChecker();
                updateChecker.CheckUpdate();

                if (UpdateMessageBox.Start(updateChecker, firstUpdateCheck) != DialogResult.Yes)
                {
                    updateTimer.Change(UpdateReCheckInterval, UpdateReCheckInterval);
                }

                firstUpdateCheck = false;
            }
        }

        public GitHubUpdateChecker CreateUpdateChecker()
        {
            return new GitHubUpdateChecker(GitHubOwner, GitHubRepo)
            {
                IsBeta = IsBeta,
                IsPortable = IsPortable,
                IncludePreRelease = CheckPreReleaseUpdates,
                Proxy = HelpersOptions.CurrentProxy.GetWebProxy()
            };
        }

        public void Dispose()
        {
            if (updateTimer != null)
            {
                updateTimer.Dispose();
                updateTimer = null;
            }
        }
    }