

namespace SharpLexer
{    
    public class UnixStyleCommentMatcher : Matcher
    {
        public UnixStyleCommentMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '#' )
                return null;

            var pos = tz.Pos;

            tz.Consume(1);

            int beginIndex = tz.Index;

            do
            {
                tz.Consume();

            } while (tz.Current != '\n' && tz.Current != '\0');



            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }
    }
}
