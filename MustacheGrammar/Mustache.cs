using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MustacheGrammar
{
    public class Mustache
    {
        private readonly Dictionary<string, string> Labels;
        private readonly List<Pattern> _patterns;

        public Mustache()
        {
            Labels = new Dictionary<string, string>();
            _patterns = new List<Pattern>
            {
                 new Pattern {Regex = new Regex(@"{{\s*(\w+)\s*}}", RegexOptions.IgnoreCase), Action = null},
                 new Pattern {Regex = new Regex(@"{{\s*(\w+ )\s*\|\s+upper\s*}}", RegexOptions.IgnoreCase), Action = null},
                 new Pattern {Regex = new Regex(@"{{\s*(\w+)\s*\|\s+lower\s*}}", RegexOptions.IgnoreCase), Action = null},
                 new Pattern {Regex = new Regex(@"{{\s*(\w+)\s*\|\s+title\s*}}", RegexOptions.IgnoreCase), Action = null},
                 new Pattern {Regex = new Regex(@"{{\s*ifnot\s*(\w+):\s*(\w+)\s*}}", RegexOptions.IgnoreCase), Action = null},
            };
        }
    }
}