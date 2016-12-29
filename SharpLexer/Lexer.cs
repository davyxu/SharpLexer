using System;
using System.Collections.Generic;

namespace SharpLexer
{
    delegate bool MatcherVisitor(Matcher m );

    public class Lexer
    {
        List<Matcher> _tokenmatcher = new List<Matcher>();
        Tokenizer _tz;
        int _index;
        TokenPos _pos = TokenPos.Init;
        string _srcName;
        Token _curr;

        internal void GetMatcherByType( Type t, MatcherVisitor callback )
        {
            foreach( var m in _tokenmatcher ){
                if ( m.GetType() != t )
                    continue;

                if (!callback(m))
                    return;
            }            
        }

        public Matcher AddMatcher(Matcher matcher)
        {
            _tokenmatcher.Add(matcher);
            return matcher;
        }

        public override string ToString()
        {
            if (_tz != null)
                return string.Format("{0} @line {1}",Peek().ToString(), _pos.Line);

            return base.ToString();
        }

        Token ReadToken( )
        {
            while( !_tz.EOF() )
            {

                foreach (var matcher in _tokenmatcher)
                {
                    var token = matcher.Match(_tz);
                    _pos = _tz.Pos;

                    if (token.Equals(Token.Nil))
                    {
                        continue;
                    }

                    // 跳过已经parse部分, 不返回外部
                    if (matcher.IsIgnored)
                        break;


                    return token;                    
                }

            }

            // EOF
            return new Token(_tz.Pos, null, null);
        }

        

        public void Start( string src, string srcName )
        {
            _srcName = srcName;

            _tz = new Tokenizer(this, src, srcName);           

        }

        public Token Read( )
        {
            _curr = ReadToken();            
            _index++;

            return _curr;
        }

        public Token Peek( )
        {
            return _curr;
        }
    }
}
