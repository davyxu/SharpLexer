﻿

namespace SharpLexer
{    
    public class UnixStyleCommentMatcher : Matcher
    {
        public UnixStyleCommentMatcher(int id)
        {
            _id = id;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.Current != '#' )
                return null;

            tz.Consume(1);

            int beginIndex = tz.Index;

            do
            {
                tz.Consume();

            } while (tz.Current != '\n' && tz.Current != '\0');



            return new Token(this, tz.Source.Substring(beginIndex, tz.Index - beginIndex));
        }
    }
}
