using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MdLiteGrammar
{
    public class MdLite
    {
        private readonly List<Pattern> _patterns;

        public MdLite()
        {
            _patterns = new List<Pattern>
            {
                new Pattern { Regex = new Regex(@"(#{1,6})((\w{1} {0,1})+)$"), Action = H},
                new Pattern { Regex = new Regex(@"\*(\w+)\*"), Action = Em},
                new Pattern { Regex = new Regex(@"\*\*(\w+)\*\*"), Action = Strong},
                new Pattern { Regex = new Regex(@"\[(\w+)\]\((\w+)\)"), Action = A},
                new Pattern { Regex = new Regex(@"!\[(\w+)\]\((\w+)\)"), Action = Img}
            };
        }

        public string Parse(string text)
        {
            bool k = true;
            text = text.Trim().Replace("  ", " ").Replace("\r\n", Environment.NewLine);
            while (k)
            {
                string textClone = text;
                foreach (var pat in _patterns)
                {
                    if (pat.Regex.IsMatch(text))
                        text = pat.Regex.Replace(text, pat.Action);
                }
                k = text != textClone;
            }
            return text;
        }

        private string H(Match match)
        {
            return string.Format("<h{0}>{1}</h{0}>", match.Groups[1].Value.Length, match.Groups[2].Value);
        }

        private string Em(Match match)
        {
            return string.Format("<em>{0}</em>", match.Groups[1].Value);
        }

        private string Strong(Match match)
        {
            return string.Format("<strong>{0}</strong>", match.Groups[1].Value);
        }

        private string A(Match match)
        {
            return string.Format("<a href=\"{0}\">{1}</a>", match.Groups[2].Value, match.Groups[1].Value);
        }

        private string Img(Match match)
        {
            return string.Format("<img src=\"{0}\" alt=\"{1}\" />", match.Groups[2].Value, match.Groups[1].Value);
        }
    }
}