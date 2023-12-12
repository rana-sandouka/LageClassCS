    public class ImageShackOptions
    {
        public string Username { get; set; }
        [JsonEncrypt]
        public string Password { get; set; }
        public bool IsPublic { get; set; }
        public string Auth_token { get; set; }
        public int ThumbnailWidth { get; set; } = 256;
        public int ThumbnailHeight { get; set; }
    }