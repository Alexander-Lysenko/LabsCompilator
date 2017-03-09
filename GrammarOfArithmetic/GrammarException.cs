using System;

namespace GrammarOfArithmetic
{
    class GrammarException : Exception
    {
        public GrammarException(string s): base(s) { }
    }
}