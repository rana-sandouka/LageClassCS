    public sealed class JpegSkiaSharpExporter : IProjectExporter
    {
        private readonly IShapeRenderer _renderer;
        private readonly IContainerPresenter _presenter;

        public JpegSkiaSharpExporter(IShapeRenderer renderer, IContainerPresenter presenter)
        {
            _renderer = renderer;
            _presenter = presenter;
        }

        public void Save(Stream stream, PageContainer container)
        {
            var info = new SKImageInfo((int)container.Width, (int)container.Height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul);
            using var bitmap = new SKBitmap(info);
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear();
                _presenter.Render(canvas, _renderer, container, 0, 0);
            }
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
            data.SaveTo(stream);
        }

        public void Save(Stream stream, DocumentContainer document)
        {
            throw new NotSupportedException("Saving documents as jpeg drawing is not supported.");
        }

        public void Save(Stream stream, ProjectContainer project)
        {
            throw new NotSupportedException("Saving projects as jpeg drawing is not supported.");
        }
    }