using System;

namespace SharpLexer
{
    public class NumeralMatcher : Matcher
    {
        public NumeralMatcher(int id)
        {
            _id = id;
        }

        public override Token Match(Tokenizer tz)
        {

            if (!Char.IsDigit(tz.Current) && tz.Current != '-' )
                return null;

            int beginIndex = tz.Index;


            do
            {
                tz.Consume();

            } while (char.IsDigit(tz.Current) || tz.Current == '.');


            return new Token(this, tz.Source.Substring( beginIndex, tz.Index - beginIndex) );
        }

    }
}
