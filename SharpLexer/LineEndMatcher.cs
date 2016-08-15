

namespace SharpLexer
{
    public class LineEndMatcher : Matcher
    {        
        public LineEndMatcher(int id)
        {
            _id = id;
        }

        public override Token Match(Tokenizer tz)
        {

            int count = 0;
            for (; ; count++)
            {
                var c = tz.Peek(count);

                if (c == '\n')
                {
                    tz.IncLine();
                }
                else if ( c == '\r')
                {

                }
                else
                {
                    break;
                }

                
            }

            if (count == 0)
                return null;

            tz.Consume(count);

            return new Token(this, string.Empty);
        }
    }
}
