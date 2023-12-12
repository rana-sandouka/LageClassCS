    public class APIChangelogUser
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("github_url")]
        public string GithubUrl { get; set; }

        [JsonProperty("osu_username")]
        public string OsuUsername { get; set; }

        [JsonProperty("user_id")]
        public int? UserId { get; set; }

        [JsonProperty("user_url")]
        public string UserUrl { get; set; }
    }