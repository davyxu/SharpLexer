

namespace SharpLexer
{
    public class CStyleCommentMatcher : Matcher
    {
        public CStyleCommentMatcher(object id)
        {
            _id = (int)id;
        }


        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '/' || tz.Peek(1) != '/')
                return Token.Nil;

            var pos = tz.Pos;

            tz.Consume(2);

            int beginIndex = tz.Index;

            do
            {
                tz.Consume();

            } while (tz.Current != '\n' && tz.Current != '\0');



            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }
    }
}
