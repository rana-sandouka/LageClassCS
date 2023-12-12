    public class AmazonS3Settings
    {
        public string AccessKeyID { get; set; }
        [JsonEncrypt]
        public string SecretAccessKey { get; set; }
        public string Endpoint { get; set; }
        public string Region { get; set; }
        public bool UsePathStyle { get; set; }
        public string Bucket { get; set; }
        public string ObjectPrefix { get; set; }
        public bool UseCustomCNAME { get; set; }
        public string CustomDomain { get; set; }
        public AmazonS3StorageClass StorageClass { get; set; }
        public bool SetPublicACL { get; set; } = true;
        public bool SignedPayload { get; set; }
        public bool RemoveExtensionImage { get; set; }
        public bool RemoveExtensionVideo { get; set; }
        public bool RemoveExtensionText { get; set; }
    }