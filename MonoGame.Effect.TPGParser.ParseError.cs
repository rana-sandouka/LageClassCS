    [Serializable]
    public class ParseError
    {
        private string file;
        private string message;
        private int code;
        private int line;
        private int col;
        private int pos;
        private int length;

        public string File { get { return file; } }
        public int Code { get { return code; } }
        public int Line { get { return line; } }
        public int Column { get { return col; } }
        public int Position { get { return pos; } }
        public int Length { get { return length; } }
        public string Message { get { return message; } }

        // just for the sake of serialization
        public ParseError()
        {
        }

        public ParseError(string message, int code, ParseNode node) : this(message, code, node.Token)
        {
        }

        public ParseError(string message, int code, Token token) : this(message, code, token.File, token.Line, token.Column, token.StartPos, token.Length)
        {
        }

        public ParseError(string message, int code) : this(message, code, string.Empty, 0, 0, 0, 0)
        {
        }

        public ParseError(string message, int code, string file, int line, int col, int pos, int length)
        {
            this.file = file;
            this.message = message;
            this.code = code;
            this.line = line;
            this.col = col;
            this.pos = pos;
            this.length = length;
        }
    }