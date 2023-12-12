    public class UserNotification
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public NotificationLevel Level { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }
    }