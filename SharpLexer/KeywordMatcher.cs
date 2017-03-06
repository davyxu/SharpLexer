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

        
        // 现在处理的结果情况:  每周  定义每, 周, 应该把 每 周单独返回

        // 特殊情况:  newAnimal  parse层, 根据两边界定符号, 将同类型的连接在一起做特殊处理

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


            tz.Consume(_word.Length);

            return new Token(pos, this, _word);

        }

    }
}
