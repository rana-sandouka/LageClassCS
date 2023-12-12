	class StartGameNotificationInfo : TraitInfo
	{
		[NotificationReference("Speech")]
		public readonly string Notification = "StartGame";

		[NotificationReference("Speech")]
		public readonly string LoadedNotification = "GameLoaded";

		[NotificationReference("Speech")]
		public readonly string SavedNotification = "GameSaved";

		public override object Create(ActorInitializer init) { return new StartGameNotification(this); }
	}