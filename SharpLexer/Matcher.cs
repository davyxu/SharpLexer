

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
        public override string ToString()
        {
            return string.Format("id: {0} {1}", _id, base.ToString());
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
