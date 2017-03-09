using System.Text.RegularExpressions;

namespace MustacheGrammar
{
    struct Pattern
    {
        public Regex Regex;
        public MatchEvaluator Action;
    }
}