

namespace SharpLexer
{
    public class WhitespaceMatcher : Matcher
    {        


        public WhitespaceMatcher(object id)
        {
            _id = (int)id;
        }

        static bool IsWhiteSpace(char c)
        {
            return c == ' ' || c == '\t';
        }


        public override Token Match(Tokenizer tz)
        {
            TokenPos pos = TokenPos.Invalid;

            int count = 0;
            for (; ; count++)
            {
                var c = tz.Peek(count);

                if (!IsWhiteSpace(c))
                    break;

                if ( pos.Equals(TokenPos.Invalid ) )
                {
                    pos = tz.Pos;
                }
                
            }

            if (count == 0)
                return null;

            tz.Consume( count );

            return new Token(pos, this, string.Empty);
        }
    }
}
