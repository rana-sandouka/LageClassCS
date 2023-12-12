    public class VgymeImageUploaderService : ImageUploaderService
    {
        public override ImageDestination EnumValue { get; } = ImageDestination.Vgyme;

        public override Icon ServiceIcon => Resources.Vgyme;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new VgymeUploader()
            {
                UserKey = config.VgymeUserKey
            };
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpVgyme;
    }