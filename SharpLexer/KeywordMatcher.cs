

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

            foreach( var c in word )
            {
                if (!IsKeyword( c ))
                {
                    throw new Exception("not keyword");
                }
            }
        }

        static bool IsKeyword(char c)
        {
            return Char.IsLetter(c) || c == '_';
        }
        public override string ToString()
        {
            return string.Format("id: {0} KeywordMatcher {1}", _id, _word);
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.CharLeft < _word.Length)
                return null;

            var pos = tz.Pos;

            int index = 0;

            foreach (var c in _word)
            {
                if (!IsKeyword(c))
                    return null;

                if (tz.Peek(index) != c)
                    return null;

                index++;
            }

            // 看尾部是否有断句特征
            var pc = tz.Peek(index);

            if (IsKeyword(pc))
                return null;

            tz.Consume(_word.Length);

            return new Token(pos, this, _word);

        }

    }
}
