    public class ZeroWidthURLShortenerService : URLShortenerService
    {
        public override UrlShortenerType EnumValue { get; } = UrlShortenerType.ZeroWidthShortener;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override URLShortener CreateShortener(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new ZeroWidthURLShortener();
        }
    }