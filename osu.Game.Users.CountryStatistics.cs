    public class CountryStatistics
    {
        [JsonProperty]
        public Country Country;

        [JsonProperty(@"code")]
        public string FlagName;

        [JsonProperty(@"active_users")]
        public long ActiveUsers;

        [JsonProperty(@"play_count")]
        public long PlayCount;

        [JsonProperty(@"ranked_score")]
        public long RankedScore;

        [JsonProperty(@"performance")]
        public long Performance;
    }