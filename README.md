# SharpLexer
简单的C#版的词法解析器

Golang版参考https://github.com/davyxu/golexer
# 使用方法

```csharp

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

```

# 备注

感觉不错请star, 谢谢!

博客: http://www.cppblog.com/sunicdavy

知乎: http://www.zhihu.com/people/xu-bo-62-87

邮箱: sunicdavy@qq.com