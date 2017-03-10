using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MustacheGrammar
{
    public class Mustache
    {
        private readonly Dictionary<string, string> _dictionary;
        private readonly List<Pattern> _patterns;

        public Mustache()
        {
            _dictionary = new Dictionary<string, string>();
            _patterns = new List<Pattern>
            {
                new Pattern { Regex = new Regex(@"{{\s*(\w+)\s*}}"), Action = Repl},
                new Pattern { Regex = new Regex(@"{{\s*(\w+)\s*\|\s*upper\s*}}"), Action = ReplUpper},
                new Pattern { Regex = new Regex(@"{{\s*(\w+)\s*\|\s*lower\s*}}"), Action = ReplLower},
                new Pattern { Regex = new Regex(@"{{\s*(\w+)\s*\|\s*title\s*}}"), Action = ReplTitle},
                new Pattern { Regex = new Regex(@"{{\s*ifnot\s*(\w+):\s*(\w+)\s*}}"), Action = ReplIfnot},
            };
        }

        public string Parse(string text, string dictionary)
        {
            ParsDictionary(dictionary);
            bool k = true;
            while (k)
            {
                string textClone = text;

                foreach (var pat in _patterns)
                {
                    text = pat.Regex.Replace(text, pat.Action);
                }
                k = text != textClone;
            }
            return text;
        }

        private void ParsDictionary(string dictionary)
        {
            string[] lines = dictionary.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            //Regex r = new Regex(@"^(.+?)-(.+?)");
            Regex r = new Regex(@"^(.+?)\:\s*""(.+?)""$");
            foreach (var l in lines)
            {
                Match m = r.Match(l.Trim());
                if (m.Success)
                {
                    _dictionary.Add(m.Groups[1].Value.Trim(), m.Groups[2].Value.Trim());
                }
                else
                {
                    throw new KeyOrValueException("Не удалось распознать пару Ключ-Значение");
                }
            }
        }

        private void KeyCorrect(string key)
        {
            if (!_dictionary.ContainsKey(key))
                throw new NotKeyException("Попытка обращения к несуществующему ключу");
        }

        private string Repl(Match mtch)
        {
            KeyCorrect(mtch.Groups[1].Value);
            return _dictionary[mtch.Groups[1].Value];
        }

        private string ReplUpper(Match mtch)
        {
            KeyCorrect(mtch.Groups[1].Value);
            return _dictionary[mtch.Groups[1].Value].ToUpper();
        }

        private string ReplLower(Match mtch)
        {
            KeyCorrect(mtch.Groups[1].Value);
            return _dictionary[mtch.Groups[1].Value].ToLower();
        }

        private string ReplTitle(Match mtch)
        {
            KeyCorrect(mtch.Groups[1].Value);
            return _dictionary[mtch.Groups[1].Value].Substring(0, 1).ToUpper()
                + (_dictionary[mtch.Groups[1].Value].Length > 1 ?
                    _dictionary[mtch.Groups[1].Value].Substring(1) : "");
        }

        private string ReplIfnot(Match mtch)
        {
            return _dictionary.ContainsKey(mtch.Groups[1].Value)
                ? _dictionary[mtch.Groups[1].Value]
                : mtch.Groups[2].Value;
        }
    }
}