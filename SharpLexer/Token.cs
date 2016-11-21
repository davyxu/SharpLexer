
namespace SharpLexer
{
    public struct TokenPos
    {
        public int Line;
        public int Col;
        public string SourceName;

        public static TokenPos Init = new TokenPos() { Line = 1, Col = 1 };
        public static TokenPos Invalid = new TokenPos(){ Line = -1, Col = -1};



        public override string ToString()
        {
            return string.Format("{0}({1})", SourceName, Line );
        }
    }

    public class Token
    {
        string _value;
        Matcher _matcher;        
        TokenPos _pos;

        public Token(TokenPos pos, Matcher matcher, string value)
        {
            _matcher = matcher;
            _value = value;
            _pos = pos;
        }

        public int MatcherID
        { 
            get {

                if (_matcher == null)
                    return 0;

                return _matcher.ID;  
            
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
