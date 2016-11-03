

namespace SharpLexer
{
    public class SignMatcher : Matcher
    {
        string _word;        

        public SignMatcher( object id, string word)            
        {
            _id = (int)id;
            _word = word;
        }

        public override Token Match(Tokenizer tz)
        {
            if (tz.CharLeft < _word.Length)
                return null;

            var pos = tz.Pos;

            int index = 0;

            foreach( var c in _word )
            {
                if (tz.Peek(index) != c)
                    return null;

                index++;
            }


            tz.Consume(_word.Length);


            return new Token(pos, this, _word);
        }

    }
}
