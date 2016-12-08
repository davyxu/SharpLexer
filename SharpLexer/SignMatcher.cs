

using System;
namespace SharpLexer
{
    public class SignMatcher : Matcher
    {
        string _word;        

        public SignMatcher( object id, string word)            
        {
            _id = (int)id;
            _word = word;

            foreach (var c in word)
            {
                if (!IsSign(c))
                {
                    throw new Exception("not sign");
                }
            }
        }

        static bool IsSign( char c )
        {
            return !char.IsLetterOrDigit(c) &&
                c != ' ' &&
                c != '\r' &&
                c != '\n';
        }

        public override string ToString()
        {
            return string.Format("id: {0} SignMatcher {1}", _id, _word);
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.CharLeft < _word.Length)
                return Token.Nil;

            var pos = tz.Pos;

            int index = 0;

            foreach( var c in _word )
            {
                if (!IsSign(c))
                    return Token.Nil;

                if (tz.Peek(index) != c)
                    return Token.Nil;

                index++;
            }            

            tz.Consume(_word.Length);

            return new Token(pos, this, _word);
        }

    }
}
