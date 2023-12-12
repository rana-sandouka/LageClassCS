    public class PuushFileUploaderService : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.Puush;

        public override Icon ServiceIcon => Resources.puush;

        public override bool CheckConfig(UploadersConfig config)
        {
            return !string.IsNullOrEmpty(config.PuushAPIKey);
        }

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new Puush(config.PuushAPIKey);
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpPuush;
    }