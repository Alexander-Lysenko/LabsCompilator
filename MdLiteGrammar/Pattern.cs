using System.Text.RegularExpressions;

namespace MdLiteGrammar
{
    struct Pattern
    {
        public Regex Regex;
        public MatchEvaluator Action;
    }
}