    public sealed class VideoBin : FileUploader
    {
        private const string URLUpload = "https://videobin.org/add";

        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("api", "1");

            UploadResult result = SendRequestFile(URLUpload, stream, fileName, "videoFile", arguments);

            result.URL = result.Response;

            return result;
        }
    }