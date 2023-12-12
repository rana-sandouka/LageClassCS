    public class UploadMetadataRequestFile
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("fileType")]
        public string FileType { get; set; }
        [JsonProperty("fileSize")]
        public int FileSize { get; set; }
    }