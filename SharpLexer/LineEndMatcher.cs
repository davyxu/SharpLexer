

namespace SharpLexer
{
    public class LineEndMatcher : Matcher
    {        
        public LineEndMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {
            TokenPos pos = TokenPos.Invalid;

            int count = 0;
            while( true )
            {
                var c = tz.Peek(0);

                if (c == '\n')
                {
                    if ( pos.Equals(TokenPos.Invalid ) )
                    {
                        pos = tz.Pos;
                    }

                    tz.Consume();
                    
                    tz.IncLine();
                    count++;

                    continue;
                }
                else if ( c == '\r')
                {

                }
                else
                {
                    break;
                }

                tz.Consume();
            }

            if (count == 0)
                return null;

            return new Token( pos, this, string.Empty);
        }
    }
}
