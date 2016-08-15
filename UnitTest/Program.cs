using SharpLexer;
using System;

namespace UnitTest
{
    enum TokenType
    {        
        Unknown = 0,
        Numeral,
        String,

        Whitespace,
        LineEnd,
        Comment,        

        Identifier,        
        
        Go,
        Semicolon,        
    }


    class Program
    {
        static void Main(string[] args)
        {
            var l = new Lexer();

            l.AddMatcher(new NumeralMatcher((int)TokenType.Numeral));
            l.AddMatcher(new StringMatcher((int)TokenType.String));

            l.AddMatcher(new WhitespaceMatcher((int)TokenType.Whitespace).Ignore());
            l.AddMatcher(new LineEndMatcher((int)TokenType.LineEnd).Ignore());
            l.AddMatcher(new UnixStyleCommentMatcher((int)TokenType.Comment).Ignore());

            l.AddMatcher(new SignMatcher((int)TokenType.Semicolon, ";"));
            l.AddMatcher(new SignMatcher((int)TokenType.Go, "go"));

            l.AddMatcher(new IdentifierMatcher((int)TokenType.Identifier));

            l.AddMatcher(new UnknownMatcher((int)TokenType.Unknown));
            l.Start(" \"a\"" + @"
	            123.3;
	            go
	            _id # comment
	            ;
	            'b'
            ");

            while( true )
            {
                var tk = l.Read();
                if (tk.MatcherID == 0)
                    break;

                Console.WriteLine(tk.ToString());
            }

        }
    }
}
