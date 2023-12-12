    public class StringLineReader
    {
        public string Text { get; private set; }
        public int Position { get; private set; }
        public int Length { get; private set; }

        public StringLineReader(string text)
        {
            Text = text;
            Length = Text.Length;
        }

        public string ReadLine()
        {
            StringBuilder builder = new StringBuilder();

            while (!string.IsNullOrEmpty(Text) && Position < Length)
            {
                char ch = Text[Position];
                builder.Append(ch);
                Position++;

                if (ch == '\r' || ch == '\n' || Position == Length)
                {
                    if (ch == '\r' && Position < Length && Text[Position] == '\n')
                    {
                        continue;
                    }

                    return builder.ToString();
                }
            }

            return null;
        }

        public string[] ReadAllLines(bool autoTrim = true)
        {
            List<string> lines = new List<string>();

            string line;

            while ((line = ReadLine()) != null)
            {
                if (autoTrim) line = line.Trim();
                lines.Add(line);
            }

            return lines.ToArray();
        }

        public void Reset()
        {
            Position = 0;
        }
    }