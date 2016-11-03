namespace SharpLexer
{
    public class UnknownMatcher : Matcher
    {
        public UnknownMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {
            var pos = tz.Pos;

            int beginIndex = tz.Index;
            tz.Consume();
            return new Token(pos, this, tz.Source.Substring( beginIndex, 1) );
        }
    }
}
