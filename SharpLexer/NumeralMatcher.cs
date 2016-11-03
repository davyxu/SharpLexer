using System;

namespace SharpLexer
{
    public class NumeralMatcher : Matcher
    {
        public NumeralMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {

            if (!Char.IsDigit(tz.Current) && tz.Current != '-' )
                return null;

            var pos = tz.Pos;

            int beginIndex = tz.Index;


            do
            {
                tz.Consume();

            } while (char.IsDigit(tz.Current) || tz.Current == '.');


            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }

    }
}
