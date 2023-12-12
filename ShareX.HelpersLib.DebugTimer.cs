    public class DebugTimer : IDisposable
    {
        public string Text { get; set; }

        private Stopwatch timer;

        public DebugTimer(string text = null)
        {
            Text = text;
            timer = Stopwatch.StartNew();
        }

        private void Write(string time, string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = Text;
            }

            if (!string.IsNullOrEmpty(text))
            {
                Debug.WriteLine(text + ": " + time);
            }
            else
            {
                Debug.WriteLine(time);
            }
        }

        public void WriteElapsedMilliseconds(string text = null)
        {
            Write(timer.Elapsed.TotalMilliseconds.ToString("0.000", CultureInfo.InvariantCulture) + " milliseconds.", text);
        }

        public void WriteElapsedSeconds(string text = null)
        {
            Write(timer.Elapsed.TotalSeconds.ToString("0.000", CultureInfo.InvariantCulture) + " seconds.", text);
        }

        public void Dispose()
        {
            WriteElapsedMilliseconds();
        }
    }