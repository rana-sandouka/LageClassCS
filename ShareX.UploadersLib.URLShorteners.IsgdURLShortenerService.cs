    public class IsgdURLShortenerService : URLShortenerService
    {
        public override UrlShortenerType EnumValue { get; } = UrlShortenerType.ISGD;

        public override bool CheckConfig(UploadersConfig config) => true;

        public override URLShortener CreateShortener(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new IsgdURLShortener();
        }
    }