    public class ShopifyPaymentsPayout : ShopifyObject
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("summary")]
        public ShopifyPaymentsPayoutSummary Summary { get; set; }
    }