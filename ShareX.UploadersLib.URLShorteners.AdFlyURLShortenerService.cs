    public class AdFlyURLShortenerService : URLShortenerService
    {
        public override UrlShortenerType EnumValue { get; } = UrlShortenerType.AdFly;

        public override Icon ServiceIcon => Resources.AdFly;

        public override bool CheckConfig(UploadersConfig config)
        {
            return !string.IsNullOrEmpty(config.AdFlyAPIKEY) && !string.IsNullOrEmpty(config.AdFlyAPIUID);
        }

        public override URLShortener CreateShortener(UploadersConfig config, TaskReferenceHelper taskInfo)
        {
            return new AdFlyURLShortener
            {
                APIKEY = config.AdFlyAPIKEY,
                APIUID = config.AdFlyAPIUID
            };
        }

        public override TabPage GetUploadersConfigTabPage(UploadersConfigForm form) => form.tpAdFly;
    }