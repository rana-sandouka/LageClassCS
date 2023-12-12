        private class ImageDownloadInfo
        {
            internal Guid ItemId { get; set; }

            internal string ImageTag { get; set; }

            internal ImageType Type { get; set; }

            internal int? Width { get; set; }

            internal int? Height { get; set; }

            internal bool IsDirectStream { get; set; }

            internal string Format { get; set; }

            internal ItemImageInfo ItemImageInfo { get; set; }
        }