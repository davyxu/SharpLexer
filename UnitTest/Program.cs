using SharpLexer;
using System;
using System.IO;

namespace UnitTest
{
    enum TokenType
    {   
        EOF = 0,
        Unknown,
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

            l.AddMatcher(new NumeralMatcher(TokenType.Numeral));
            l.AddMatcher(new StringMatcher(TokenType.String));

            l.AddMatcher(new WhitespaceMatcher(TokenType.Whitespace).Ignore());
            l.AddMatcher(new LineEndMatcher(TokenType.LineEnd).Ignore());
            l.AddMatcher(new UnixStyleCommentMatcher(TokenType.Comment).Ignore());

            l.AddMatcher(new SignMatcher(TokenType.Semicolon, ";"));
            l.AddMatcher(new SignMatcher(TokenType.Go, "go"));

            l.AddMatcher(new IdentifierMatcher(TokenType.Identifier));

            l.AddMatcher(new UnknownMatcher(TokenType.Unknown));
            l.Start(" \"a\"" + @"
	            123.3;
	            go
	            _id # comment
	            ;
	            'b'
            ");

            //l.Start(File.ReadAllText("a.txt"));

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
