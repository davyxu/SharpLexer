# SharpLexer
简单的C#版的词法解析器

Golang版参考https://github.com/davyxu/golexer
# 使用方法

```csharp

    var l = new Lexer();

    l.AddMatcher(new NumeralMatcher(TokenType.Numeral));
    l.AddMatcher(new StringMatcher(TokenType.String));

    l.AddMatcher(new WhitespaceMatcher(TokenType.Whitespace).Ignore());
    l.AddMatcher(new LineEndMatcher(TokenType.LineEnd).Ignore());
    l.AddMatcher(new UnixStyleCommentMatcher(TokenType.Comment).Ignore());

    l.AddMatcher(new SignMatcher(TokenType.Semicolon, ";"));
    l.AddMatcher(new SignMatcher(TokenType.Dot, "."));
    l.AddMatcher(new KeywordMatcher(TokenType.Go, "go"));
    l.AddMatcher(new KeywordMatcher(TokenType.XX, "xx"));

    l.AddMatcher(new IdentifierMatcher(TokenType.Identifier));

    l.AddMatcher(new UnknownMatcher(TokenType.Unknown));
    l.Start(" \"a\"" + @"
	    123.3;
	    gonew.xx
	    _id # comment
	    ;
	    'b'
    ", "");

    while( true )
    {
        var tk = l.Read();
        if (tk.MatcherID == 0)
            break;

        Console.WriteLine(tk.ToString());
    }

```

# 备注

感觉不错请star, 谢谢!

博客: http://www.cppblog.com/sunicdavy

知乎: http://www.zhihu.com/people/xu-bo-62-87

邮箱: sunicdavy@qq.com