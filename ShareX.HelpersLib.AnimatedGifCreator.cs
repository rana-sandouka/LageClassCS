    public class AnimatedGifCreator : IDisposable
    {
        public string FilePath { get; private set; }
        public int Delay { get; private set; }
        public int Repeat { get; private set; }
        public int FrameCount { get; private set; }

        private FileStream stream;

        public AnimatedGifCreator(string filePath, int delay, int repeat = 0)
        {
            FilePath = filePath;
            Delay = delay;
            Repeat = repeat;
        }

        public void AddFrame(Image img, GIFQuality quality = GIFQuality.Default)
        {
            GifClass gif = new GifClass();
            gif.LoadGifPicture(img, quality);

            if (stream == null)
            {
                stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
                stream.Write(CreateHeaderBlock());
                stream.Write(gif.ScreenDescriptor.ToArray());
                stream.Write(CreateApplicationExtensionBlock(Repeat));
            }

            stream.Write(CreateGraphicsControlExtensionBlock(Delay));
            stream.Write(gif.ImageDescriptor.ToArray());
            stream.Write(gif.ColorTable.ToArray());
            stream.Write(gif.ImageData.ToArray());

            FrameCount++;
        }

        public void AddFrame(string path, GIFQuality quality = GIFQuality.Default)
        {
            using (Bitmap bmp = ImageHelpers.LoadImage(path))
            {
                AddFrame(bmp, quality);
            }
        }

        private void Finish()
        {
            if (stream != null)
            {
                stream.WriteByte(0x3B); // Image terminator
                stream.Dispose();
            }
        }

        public void Dispose()
        {
            Finish();
        }

        private byte[] CreateHeaderBlock()
        {
            return new byte[] { (byte)'G', (byte)'I', (byte)'F', (byte)'8', (byte)'9', (byte)'a' };
        }

        private byte[] CreateApplicationExtensionBlock(int repeat)
        {
            byte[] buffer = new byte[19];
            buffer[0] = 0x21; // Extension introducer
            buffer[1] = 0xFF; // Application extension
            buffer[2] = 0x0B; // Size of block
            buffer[3] = (byte)'N'; // NETSCAPE2.0
            buffer[4] = (byte)'E';
            buffer[5] = (byte)'T';
            buffer[6] = (byte)'S';
            buffer[7] = (byte)'C';
            buffer[8] = (byte)'A';
            buffer[9] = (byte)'P';
            buffer[10] = (byte)'E';
            buffer[11] = (byte)'2';
            buffer[12] = (byte)'.';
            buffer[13] = (byte)'0';
            buffer[14] = 0x03; // Size of block
            buffer[15] = 0x01; // Loop indicator
            buffer[16] = (byte)(repeat % 0x100); // Number of repetitions
            buffer[17] = (byte)(repeat / 0x100); // 0 for endless loop
            buffer[18] = 0x00; // Block terminator
            return buffer;
        }

        private byte[] CreateGraphicsControlExtensionBlock(int delay)
        {
            byte[] buffer = new byte[8];
            buffer[0] = 0x21; // Extension introducer
            buffer[1] = 0xF9; // Graphic control extension
            buffer[2] = 0x04; // Size of block
            buffer[3] = 0x09; // Flags: reserved, disposal method, user input, transparent color
            buffer[4] = (byte)((delay / 10) % 0x100); // Delay time low byte
            buffer[5] = (byte)(delay / 10 / 0x100); // Delay time high byte
            buffer[6] = 0xFF; // Transparent color index
            buffer[7] = 0x00; // Block terminator
            return buffer;
        }
    }