

namespace SharpLexer
{
    public class WhitespaceMatcher : Matcher
    {        
        bool IsWhiteSpace( char c )
        {
            return c == ' ' || c == '\t';
        }

        public WhitespaceMatcher(int id)
        {
            _id = id;
        }


        public override Token Match(Tokenizer tz)
        {
            
            int count = 0;
            for (; ; count++)
            {
                var c = tz.Peek(count);

                if (!IsWhiteSpace(c))
                    break;                
            }

            if (count == 0)
                return null;

            tz.Consume( count );

            return new Token(this, string.Empty);
        }
    }
}
