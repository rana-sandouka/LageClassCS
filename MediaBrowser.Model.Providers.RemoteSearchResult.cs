    public class RemoteSearchResult : IHasProviderIds
    {
        public RemoteSearchResult()
        {
            ProviderIds = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Artists = Array.Empty<RemoteSearchResult>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the provider ids.
        /// </summary>
        /// <value>The provider ids.</value>
        public Dictionary<string, string> ProviderIds { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? ProductionYear { get; set; }

        public int? IndexNumber { get; set; }

        public int? IndexNumberEnd { get; set; }

        public int? ParentIndexNumber { get; set; }

        public DateTime? PremiereDate { get; set; }

        public string ImageUrl { get; set; }

        public string SearchProviderName { get; set; }

        public string Overview { get; set; }

        public RemoteSearchResult AlbumArtist { get; set; }

        public RemoteSearchResult[] Artists { get; set; }
    }