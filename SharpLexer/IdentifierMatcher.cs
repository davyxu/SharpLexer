using System;

namespace SharpLexer
{
    public class IdentifierMatcher : Matcher
    {
        public IdentifierMatcher( int id )
        {
            _id = id;
        }

        public override Token Match(Tokenizer tz)
        {

            if (!( Char.IsLetter(tz.Current) || tz.Current == '_' ))
                return null;

            int beginIndex = tz.Index;


            do
            {
                tz.Consume();

            } while (char.IsLetterOrDigit(tz.Current) || tz.Current == '_');


            return new Token( this, tz.Source.Substring( beginIndex, tz.Index - beginIndex) );
        }

    }
}
