

namespace SharpLexer
{
    public abstract class Matcher
    {
        bool _ignore;
        protected int _id;

        public abstract Token Match(Tokenizer tz);
        public Matcher Ignore( )
        {
            _ignore = true;
            return this;
        }

        public int ID
        {
            get { return _id; }
        }

        public bool IsIgnored
        {
            get { return _ignore; }
        }
    }
}
