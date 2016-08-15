namespace SharpLexer
{
    public class UnknownMatcher : Matcher
    {
        public UnknownMatcher(int id)
        {
            _id = id;
        }

        public override Token Match(Tokenizer tz)
        {
            int beginIndex = tz.Index;
            tz.Consume();
            return new Token(this, tz.Source.Substring( beginIndex, 1) );
        }
    }
}
