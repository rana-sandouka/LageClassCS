    public abstract class APIDownloadRequest : APIRequest
    {
        private string filename;

        /// <summary>
        /// Used to set the extension of the file returned by this request.
        /// </summary>
        protected virtual string FileExtension { get; } = @".tmp";

        protected override WebRequest CreateWebRequest()
        {
            var file = Path.GetTempFileName();

            File.Move(file, filename = Path.ChangeExtension(file, FileExtension));

            var request = new FileWebRequest(filename, Uri);
            request.DownloadProgress += request_Progress;
            return request;
        }

        private void request_Progress(long current, long total) => API.Schedule(() => Progressed?.Invoke(current, total));

        protected void TriggerSuccess(string filename)
        {
            if (this.filename != null)
                throw new InvalidOperationException("Attempted to trigger success more than once");

            this.filename = filename;

            TriggerSuccess();
        }

        internal override void TriggerSuccess()
        {
            base.TriggerSuccess();
            Success?.Invoke(filename);
        }

        public event APIProgressHandler Progressed;

        public new event APISuccessHandler<string> Success;
    }