    public sealed class CustomImageUploader : ImageUploader
    {
        private CustomUploaderItem uploader;

        public CustomImageUploader(CustomUploaderItem customUploaderItem)
        {
            uploader = customUploaderItem;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();
            CustomUploaderInput input = new CustomUploaderInput(fileName, "");

            if (uploader.Body == CustomUploaderBody.MultipartFormData)
            {
                result = SendRequestFile(uploader.GetRequestURL(input), stream, fileName, uploader.GetFileFormName(), uploader.GetArguments(input),
                    uploader.GetHeaders(input), null, uploader.RequestMethod);
            }
            else if (uploader.Body == CustomUploaderBody.Binary)
            {
                result.Response = SendRequest(uploader.RequestMethod, uploader.GetRequestURL(input), stream, RequestHelpers.GetMimeType(fileName),
                    null, uploader.GetHeaders(input));
            }
            else
            {
                throw new Exception("Unsupported request format: " + uploader.Body);
            }

            try
            {
                uploader.ParseResponse(result, LastResponseInfo, input);
            }
            catch (Exception e)
            {
                Errors.Add(Resources.CustomFileUploader_Upload_Response_parse_failed_ + Environment.NewLine + e);
            }

            return result;
        }
    }