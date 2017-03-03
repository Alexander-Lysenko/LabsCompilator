using System;
using System.Text.RegularExpressions;

namespace AsmGrammar
{
    struct Pattern
    {
        public Regex Regex;
        public Action<State, string[]> Action;
    }
}