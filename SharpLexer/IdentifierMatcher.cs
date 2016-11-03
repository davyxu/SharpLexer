using System;

namespace SharpLexer
{
    public class IdentifierMatcher : Matcher
    {
        public IdentifierMatcher( object id )
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {

            if (!( Char.IsLetter(tz.Current) || tz.Current == '_' ))
                return null;

            var pos = tz.Pos;

            int beginIndex = tz.Index;


            do
            {
                tz.Consume();

            } while (char.IsLetterOrDigit(tz.Current) || tz.Current == '_');


            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }

    }
}
