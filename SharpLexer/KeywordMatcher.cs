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

        // 处理情况1:  newAnimal  定义new, 把new匹配后返回new, 未完全匹配完
        // 处理情况2:  每周  定义每, 周, 应该把 每 周单独返回

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

            // 其他的同类型的matcher需要解析, 返回当前解析对象
            bool needParse = false;

            tz.Lexer.GetMatcherByType(typeof(KeywordMatcher), delegate(Matcher m)
            {
                var km = m as KeywordMatcher;
                if (km._word.IndexOf(pc) == 0)
                {
                    needParse = true;
                    return false;
                }


                return true;
            });

            // 关键字还在继续, 且后方没有人需要, 所以需要读完整句
            if (IsKeyword(index, pc) && !needParse)
                return Token.Nil;

            tz.Consume(_word.Length);

            return new Token(pos, this, _word);

        }

    }
}
