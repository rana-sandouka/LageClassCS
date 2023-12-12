    public class DropboxLinkMetadata
    {
        [JsonProperty(".tag")]
        public string tag { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public DropboxLinkMetadataPermissions link_permissions { get; set; }
        public string client_modified { get; set; }
        public string server_modified { get; set; }
        public string rev { get; set; }
        public int size { get; set; }
        public string id { get; set; }
        public string path_lower { get; set; }
        public DropboxLinkMetadataTeamMemberInfo team_member_info { get; set; }
    }