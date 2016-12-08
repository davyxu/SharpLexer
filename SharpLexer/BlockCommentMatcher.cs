

namespace SharpLexer
{    
    public class BlockCommentMatcher : Matcher
    {
        public BlockCommentMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '/' || tz.Peek(1) != '*')
                return Token.Nil;

            var pos = tz.Pos;

            tz.Consume(2);

            int beginIndex = tz.Index;

            do
            {
                tz.Consume();

            } while (tz.Current != '*' || tz.Peek(1) != '/');

            var endIndex = tz.Index;

            tz.Consume(2);

            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - endIndex ));
        }
    }
}
