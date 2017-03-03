using System;
using System.Text.RegularExpressions;

namespace MustacheGrammar
{
    struct Pattern
    {
        public Regex Regex;
        public Action<State, string[]> Action;
    }
}