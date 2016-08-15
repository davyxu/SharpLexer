

namespace SharpLexer
{
    public class CStyleCommentMatcher : Matcher
    {
        public CStyleCommentMatcher(int id)
        {
            _id = id;
        }


        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '/' || tz.Peek(1) != '/')
                return null;

            tz.Consume(2);

            int beginIndex = tz.Index;

            do
            {
                tz.Consume();

            } while (tz.Current != '\n' && tz.Current != '\0');



            return new Token(this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }
    }
}
