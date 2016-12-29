
namespace SharpLexer
{
    public struct TokenPos
    {
        public int Line;
        public int Col;
        public string SourceName;

        public static TokenPos Init = new TokenPos() { Line = 1, Col = 1 };
        public static TokenPos Invalid = new TokenPos(){ Line = -1, Col = -1};

        public override bool Equals(object obj)
        {
            var other = (TokenPos)obj;

            return other.Line == this.Line &&
                other.Col == this.Col &&
                other.SourceName == this.SourceName;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", SourceName, Line );
        }
    }

    public struct Token
    {
        string _value;
        Matcher _matcher;        
        TokenPos _pos;

        public static Token Nil = new Token(TokenPos.Invalid, null, string.Empty);

        public Token(TokenPos pos, Matcher matcher, string value)
        {
            _matcher = matcher;
            _value = value;
            _pos = pos;
        }

        public override bool Equals(object obj)
        {
            var other = (Token)obj;


            return other._value == this.Value && 
                other._matcher == this._matcher &&                
                other._pos.Equals(this._pos);
        }

        public int MatcherID
        { 
            get {

                if (_matcher == null)
                    return 0;

                return _matcher.ID;  
            
            } 
        }

        public string MatcherName
        {
            get
            {

                if (_matcher == null)
                    return string.Empty;

                return _matcher.GetType().Name;

            }
        }

        public string Value
        {
            get { return _value;  }
        }

        public TokenPos Pos
        {
            get { return _pos; }
        }
        

        public float ToNumber()
        {
            return float.Parse(_value);
        }

        public override string ToString()
        {
            if (_matcher == null)
            {
                return "EOF";
            }

            return string.Format("{0} {1} {2}", _pos, _matcher.GetType().Name, Value);            
        }
    }

}
