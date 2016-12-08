using System.Text;
namespace SharpLexer
{
    public class StringMatcher : Matcher
    {
        StringBuilder _builder = new StringBuilder();

        public StringMatcher(object id )            
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '"' && tz.Current != '\'')
                return Token.Nil;

            var pos = tz.Pos;

            tz.Consume(1);

            _builder.Length = 0;


            bool escaping = false;

            do
            {
                // 将转义符xian
                if ( escaping )
                {
                    switch (tz.Current)
                    {
                        case 'n':
                            {
                                _builder.Append("\n");
                            }
                            break;
                        case 'r':
                            {
                                _builder.Append("\r");
                            }
                            break;
                        default:
                            {
                                _builder.Append('\\');
                                _builder.Append(tz.Current);
                            }
                            break;
                    }

                    escaping = false;
                }
                else
                {
                    if ( tz.Current == '\\')
                    {
                        escaping = true;
                    }
                    else
                    {
                        _builder.Append(tz.Current);
                    }
                }

  


                tz.Consume();

            } while (tz.Current != '\n' && tz.Current != '\0' && tz.Current != '\'' && tz.Current != '"');


            tz.Consume();

            return new Token(pos, this, _builder.ToString());
        }

    }
}
