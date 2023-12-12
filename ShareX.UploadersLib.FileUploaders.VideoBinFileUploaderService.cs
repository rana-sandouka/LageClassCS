    public class VideoBinFileUploaderService : FileUploaderService
    {
        public override FileDestination EnumValue { get; } = FileDestination.VideoBin;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override GenericUploader CreateUploader(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new VideoBin();
        }
    }