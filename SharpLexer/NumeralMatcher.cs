﻿using System;

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
                return Token.Nil;

            var pos = tz.Pos;

            int beginIndex = tz.Index;


            do
            {
                tz.Consume();

            } while (char.IsDigit(tz.Current) || tz.Current == '.');


            return new Token(pos, this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }

    }

    public class PositiveNumeralMatcher : Matcher
    {
        public PositiveNumeralMatcher(object id)
        {
            _id = (int)id;
        }

        public override Token Match(Tokenizer tz)
        {

            if (!Char.IsDigit(tz.Current)  )
                return Token.Nil;

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
