    public static class UploadersConfigValidator
    {
        public static bool Validate<T>(int index, UploadersConfig config)
        {
            Enum destination = (Enum)Enum.ToObject(typeof(T), index);

            if (destination is ImageDestination)
            {
                return Validate((ImageDestination)destination, config);
            }

            if (destination is TextDestination)
            {
                return Validate((TextDestination)destination, config);
            }

            if (destination is FileDestination)
            {
                return Validate((FileDestination)destination, config);
            }

            if (destination is UrlShortenerType)
            {
                return Validate((UrlShortenerType)destination, config);
            }

            if (destination is URLSharingServices)
            {
                return Validate((URLSharingServices)destination, config);
            }

            return true;
        }

        public static bool Validate(ImageDestination destination, UploadersConfig config)
        {
            if (destination == ImageDestination.FileUploader) return true;
            return UploaderFactory.ImageUploaderServices[destination].CheckConfig(config);
        }

        public static bool Validate(TextDestination destination, UploadersConfig config)
        {
            if (destination == TextDestination.FileUploader) return true;
            return UploaderFactory.TextUploaderServices[destination].CheckConfig(config);
        }

        public static bool Validate(FileDestination destination, UploadersConfig config)
        {
            return UploaderFactory.FileUploaderServices[destination].CheckConfig(config);
        }

        public static bool Validate(UrlShortenerType destination, UploadersConfig config)
        {
            return UploaderFactory.URLShortenerServices[destination].CheckConfig(config);
        }

        public static bool Validate(URLSharingServices destination, UploadersConfig config)
        {
            return UploaderFactory.URLSharingServices[destination].CheckConfig(config);
        }
    }