using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MdLiteGrammar
{
    public class Mustache
    {

        public string Pars(string text)
        {
            bool k = true;
            while (k)
            {
                string textClone = text;

                Regex r = new Regex(@"«\*(\w+)\*»");
                text = r.Replace(text, Em);

                r = new Regex(@"«\*\*(\w+)\*\*»");
                text = r.Replace(text, Strong);

                r = new Regex(@"«\[(\w+)\]\((\w+)\)»");
                text = r.Replace(text, A);

                r = new Regex(@"«!\[(\w+)\]\((\w+)\)»");
                text = r.Replace(text, Img);

                k = text != textClone;
            }
            return text;
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