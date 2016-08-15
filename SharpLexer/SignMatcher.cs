

namespace SharpLexer
{
    public class SignMatcher : Matcher
    {
        string _word;        

        public SignMatcher( int id, string word)            
        {
            _id = id;
            _word = word;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.CharLeft < _word.Length)
                return null;

            int index = 0;

            foreach( var c in _word )
            {
                if (tz.Peek(index) != c)
                    return null;

                index++;
            }


            tz.Consume(_word.Length);


            return new Token(this, _word );
        }

    }
}
