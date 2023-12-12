    public class ImageFilesCache : IDisposable
    {
        private Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap>();

        public Bitmap GetImage(string filePath)
        {
            Bitmap bmp = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (images.ContainsKey(filePath))
                {
                    return images[filePath];
                }

                bmp = ImageHelpers.LoadImage(filePath);

                if (bmp != null)
                {
                    images.Add(filePath, bmp);
                }
            }

            return bmp;
        }

        public Bitmap GetFileIconAsImage(string filePath, bool isSmallIcon = true)
        {
            Bitmap bmp = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (images.ContainsKey(filePath))
                {
                    return images[filePath];
                }

                using (Icon icon = NativeMethods.GetFileIcon(filePath, isSmallIcon))
                {
                    if (icon != null && icon.Width > 0 && icon.Height > 0)
                    {
                        bmp = icon.ToBitmap();

                        if (bmp != null)
                        {
                            images.Add(filePath, bmp);
                        }
                    }
                }
            }

            return bmp;
        }

        public void Clear()
        {
            if (images != null)
            {
                Dispose();

                images.Clear();
            }
        }

        public void Dispose()
        {
            if (images != null)
            {
                foreach (Bitmap bmp in images.Values)
                {
                    if (bmp != null)
                    {
                        bmp.Dispose();
                    }
                }
            }
        }
    }