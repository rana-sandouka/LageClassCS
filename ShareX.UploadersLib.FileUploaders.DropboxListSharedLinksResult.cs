    public class DropboxListSharedLinksResult
    {
        public DropboxLinkMetadata[] links { get; set; }
        public bool has_more { get; set; }
        public string cursor { get; set; }
    }