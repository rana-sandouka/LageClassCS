    public class ImageInfo : IDisposable
    {
        public Bitmap Image { get; set; }
        public string WindowTitle { get; set; }
        public string ProcessName { get; set; }

        public ImageInfo()
        {
        }

        public ImageInfo(Bitmap image)
        {
            Image = image;
        }

        public void UpdateInfo(WindowInfo windowInfo)
        {
            if (windowInfo != null)
            {
                WindowTitle = windowInfo.Text;
                ProcessName = windowInfo.ProcessName;
            }
        }

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
            }
        }
    }