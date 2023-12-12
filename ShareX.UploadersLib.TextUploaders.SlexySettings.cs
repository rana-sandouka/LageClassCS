    public class SlexySettings
    {
        /// <summary>language</summary>
        public string TextFormat { get; set; }

        /// <summary>author</summary>
        public string Author { get; set; }

        /// <summary>permissions</summary>
        public Privacy Visibility { get; set; }

        /// <summary>desc</summary>
        public string Description { get; set; }

        /// <summary>linenumbers</summary>
        public bool LineNumbers { get; set; }

        /// <summary>expire</summary>
        [Description("Expiration time with seconds. Example: 0 = Forever, 60 = 1 minutes, 3600 = 1 hour, 2592000 = 1 month")]
        public string Expiration { get; set; }

        public SlexySettings()
        {
            TextFormat = "text";
            Author = "";
            Visibility = Privacy.Private;
            Description = "";
            LineNumbers = true;
            Expiration = "2592000";
        }
    }