using System.Collections.Generic;

namespace SharpLexer
{
    public class Lexer
    {
        IList<Matcher> _tokenmatcher = new List<Matcher>();
        IEnumerator<Token> _tokeniter;
        int _index;
        TokenPos _pos = TokenPos.Init;
        string _srcName;

        public void AddMatcher(Matcher matcher)
        {
            _tokenmatcher.Add(matcher);
        }

        public override string ToString()
        {
            if (_tokeniter != null)
                return string.Format("{0} @line {1}",Peek().ToString(), _pos.Line);

            return base.ToString();
        }

        public IEnumerable<Token> Tokenize(string source, string srcName)
        {
            var tz = new Tokenizer(source, srcName);

            while( !tz.EOF() )
            {

                foreach (var matcher in _tokenmatcher)
                {
                    var token = matcher.Match(tz);
                    _pos = tz.Pos;

                    if (token == null)
                    {
                        continue;
                    }

                    // 跳过已经parse部分, 不返回外部
                    if (matcher.IsIgnored)
                        break;


                    yield return token;

                    break;
                }

            }

            // EOF
            yield return new Token(tz.Pos, null, null);
        }

        public void Start( string src, string srcName )
        {
            _srcName = srcName;

            _tokeniter = Tokenize(src, srcName).GetEnumerator();

            _tokeniter.MoveNext();

        }

        public Token Read( )
        {
            var tk = _tokeniter.Current;

            _tokeniter.MoveNext();
            _index++;

            return tk;
        }

        public Token Peek( )
        {
            return _tokeniter.Current;
        }
    }
}
