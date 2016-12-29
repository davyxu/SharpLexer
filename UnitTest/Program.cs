using SharpLexer;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
        Dot,
        Every,
        Week,
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
            l.AddMatcher(new BlockCommentMatcher(TokenType.Comment).Ignore());

            l.AddMatcher(new SignMatcher(TokenType.Semicolon, ";"));
            l.AddMatcher(new SignMatcher(TokenType.Dot, "."));
            l.AddMatcher(new KeywordMatcher(TokenType.Go, "go"));
            l.AddMatcher(new KeywordMatcher(TokenType.Every, "每"));
            l.AddMatcher(new KeywordMatcher(TokenType.Week, "周"));

            l.AddMatcher(new IdentifierMatcher(TokenType.Identifier));

            l.AddMatcher(new UnknownMatcher(TokenType.Unknown));
            l.Start(" \'a\'" + @"
	            123.3;
                Base64Text
	            gonew.每周
	            _id # comment                
            /*  这里
    是
    多行
    的注释
*/
	            ;
	            'b'
            ", "");
            

            //l.Start(File.ReadAllText("a.txt"), string.Empty);

            var rightAnswer = @"===
MatcherName: 'StringMatcher' Value: 'a'
MatcherName: 'NumeralMatcher' Value: '123.3'
MatcherName: 'SignMatcher' Value: ';'
MatcherName: 'IdentifierMatcher' Value: 'Base64Text'
MatcherName: 'IdentifierMatcher' Value: 'gonew'
MatcherName: 'SignMatcher' Value: '.'
MatcherName: 'KeywordMatcher' Value: '每'
MatcherName: 'KeywordMatcher' Value: '周'
MatcherName: 'IdentifierMatcher' Value: '_id'
MatcherName: 'SignMatcher' Value: ';'
MatcherName: 'StringMatcher' Value: 'b'
===
";

            var sb = new StringBuilder();
            sb.AppendLine("===");

            while( true )
            {
                var tk = l.Read();
                if (tk.MatcherID == 0)
                    break;

                sb.AppendLine(string.Format("MatcherName: '{0}' Value: '{1}'", tk.MatcherName, tk.Value));
                
                Console.WriteLine(tk.ToString());
            }
            sb.AppendLine("===");

            Debug.Write(sb.ToString());

            if (sb.ToString() != rightAnswer)
            {
                Debug.Assert(false);
            }

        }
    }
}
