using System;

namespace SharpLexer
{
    public class KeywordMatcher : Matcher
    {
        string _word;

        public KeywordMatcher(object id, string word)            
        {
            _id = (int)id;
            _word = word;

            int index = 0;
            foreach( var c in word )
            {
                if (!IsKeyword( index, c ))
                {
                    throw new Exception("not keyword");
                }

                index++;
            }
        }

        static bool IsKeyword(int index, char c)
        {
            bool basic = Char.IsLetter(c) || c == '_';

            if (index == 0)
            {
                return basic;
            }

            return basic || Char.IsDigit(c);
        }

        public override string ToString()
        {
            return string.Format("id: {0} KeywordMatcher {1}", _id, _word);
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.CharLeft < _word.Length)
                return Token.Nil;

            var pos = tz.Pos;

            int index = 0;

            foreach (var c in _word)
            {
                if (!IsKeyword(index, c))
                    return Token.Nil;

                if (tz.Peek(index) != c)
                    return Token.Nil;

                index++;
            }

            // 看尾部是否有断句特征
            var pc = tz.Peek(index);

            if (IsKeyword(index, pc))
                return Token.Nil;

            tz.Consume(_word.Length);

            return new Token(pos, this, _word);

        }

    }
}
