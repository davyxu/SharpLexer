
namespace SharpLexer
{
    public class Tokenizer
    {
        string _source;
        Lexer _lexer;
        public Tokenizer(Lexer lexer, string src, string srcName)
        {
            _lexer = lexer;
            Pos = TokenPos.Init;
            var p = Pos;
            p.SourceName = srcName;
            Pos = p;
            _source = src;
        }

        internal Lexer Lexer
        {
            get { return _lexer; }
        }

        public override string ToString()
        {
            return Current.ToString();
        }

        public int Index { get; set; }

        public TokenPos Pos { get; set;}        

        public string Source { get { return _source; } }

        public char Current
        {
            get {
                if (EOF(0))
                    return '\0';

                return _source[Index];
            }
        }

        public int CharLeft
        {
            get { return _source.Length - Index;  }
        }

        public char Peek(int offset)
        {
            if (EOF(offset))
                return '\0';

            return _source[Index + offset ];
        }

       


        public void Consume( int count = 1 )
        {
            Index += count ;
            var p = Pos;
            p.Col += count;
            Pos = p;            
        }

        public bool EOF(int offset = 0)
        {
            return Index + offset >= _source.Length;
        }

        public void IncLine( )
        {
            var p = Pos;            
            p.Line++;
            p.Col = 1;
            Pos = p;
        }
    }
}
