	public class DesktopApp : BK {
		public static string In = "20sw479alxyc1";
		
		private List<Integration> integrations = new List<Integration>();
		private TwitchIntegration twitchIntegration;
		
		public DesktopApp() : base(Display.Width * 3, Display.Height * 3, !BK.Version.Dev) {
			
		}

		protected override void Initialize() {
			base.Initialize();

			integrations.Add(new DiscordIntegration());
			integrations.Add(new SteamIntegration());
			integrations.Add(twitchIntegration = new TwitchIntegration());

			foreach (var i in integrations) {
				i.Init();
			}
			
			AssetsLoaded += () => {
				foreach (var i in integrations) {
					i.PostInit();
				}
			};
		}

		protected override void Destroy() {
			foreach (var i in integrations) {
				i.Destroy();
			}
			
			integrations.Clear();
			
			base.Destroy();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);
			
			foreach (var i in integrations) {
				i.Update(Delta);
			}
		}

		public override void RenderUi() {
			base.RenderUi();
			twitchIntegration?.Render();
		}
	}