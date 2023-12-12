    public class MediaFireFileUploaderService : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.MediaFire;

        public override Icon ServiceIcon => Resources.MediaFire;

        public override bool CheckConfig(UploadersConfig config)
        {
            return !string.IsNullOrEmpty(config.MediaFireUsername) && !string.IsNullOrEmpty(config.MediaFirePassword);
        }

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new MediaFire(APIKeys.MediaFireAppId, APIKeys.MediaFireApiKey, config.MediaFireUsername, config.MediaFirePassword)
            {
                UploadPath = NameParser.Parse(NameParserType.URL, config.MediaFirePath),
                UseLongLink = config.MediaFireUseLongLink
            };
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpMediaFire;
    }