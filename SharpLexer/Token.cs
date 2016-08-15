
namespace SharpLexer
{

    public class Token
    {
        string _value;
        Matcher _matcher;

        public Token(Matcher matcher, string value)
        {
            _matcher = matcher;
            _value = value;
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

            return _matcher.GetType().Name + " " + Value;
        }
    }

}
