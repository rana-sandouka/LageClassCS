    public class InventoryLevelListFilter : ListFilter<InventoryLevel>
    {
        [JsonProperty("inventory_item_ids")]
        public IEnumerable<long> InventoryItemIds { get; set; }

        [JsonProperty("location_ids")]
        public IEnumerable<long> LocationIds { get; set; }
        
        [JsonProperty("updated_at_min")]
        public DateTimeOffset? UpdatedAtMin { get; set; }
    }