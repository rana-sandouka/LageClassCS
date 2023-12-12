    public sealed class YouTube : FileUploader, IOAuth2
    {
        public OAuth2Info AuthInfo => googleAuth.AuthInfo;
        public YouTubeVideoPrivacy PrivacyType { get; set; }
        public bool UseShortenedLink { get; set; }

        private GoogleOAuth2 googleAuth;

        public YouTube(OAuth2Info oauth)
        {
            googleAuth = new GoogleOAuth2(oauth, this)
            {
                Scope = "https://www.googleapis.com/auth/youtube.upload"
            };
        }

        public bool RefreshAccessToken()
        {
            return googleAuth.RefreshAccessToken();
        }

        public bool CheckAuthorization()
        {
            return googleAuth.CheckAuthorization();
        }

        public string GetAuthorizationURL()
        {
            return googleAuth.GetAuthorizationURL();
        }

        public bool GetAccessToken(string code)
        {
            return googleAuth.GetAccessToken(code);
        }

        private string GetMetadata(string title)
        {
            object metadata = new
            {
                snippet = new
                {
                    title = title
                },
                status = new
                {
                    privacyStatus = PrivacyType.ToString()
                }
            };

            return JsonConvert.SerializeObject(metadata);
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            if (!CheckAuthorization()) return null;

            string title = Path.GetFileNameWithoutExtension(fileName);
            string metadata = GetMetadata(title);

            UploadResult result = SendRequestFile("https://www.googleapis.com/upload/youtube/v3/videos?part=id,snippet,status", stream, fileName, "file",
                headers: googleAuth.GetAuthHeaders(), relatedData: metadata);

            if (!string.IsNullOrEmpty(result.Response))
            {
                YouTubeVideo video = JsonConvert.DeserializeObject<YouTubeVideo>(result.Response);

                if (video != null)
                {
                    if (UseShortenedLink)
                    {
                        result.URL = $"https://youtu.be/{video.id}";
                    }
                    else
                    {
                        result.URL = $"https://www.youtube.com/watch?v={video.id}";
                    }

                    switch (video.status.uploadStatus)
                    {
                        case YouTubeVideoStatus.UploadFailed:
                            Errors.Add("Upload failed: " + video.status.failureReason);
                            break;
                        case YouTubeVideoStatus.UploadRejected:
                            Errors.Add("Upload rejected: " + video.status.rejectionReason);
                            break;
                    }
                }
            }

            return result;
        }
    }