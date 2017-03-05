using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AsmGrammar
{
    public class Asm
    {
        private readonly State _state;
        private readonly List<Pattern> _patterns;
        private string _syscall;

        public Asm()
        {
            _state = new State();
            _patterns = new List<Pattern>
            {
                new Pattern {Regex = new Regex(@"^LD\s+r(\d+),\s*#(\d+)$", RegexOptions.IgnoreCase), Action = LD},
                new Pattern {Regex = new Regex(@"^MOV\s+r(\d+),\s*r(\d+)$", RegexOptions.IgnoreCase), Action = MOV},
                new Pattern {Regex = new Regex(@"^ADD\s+r(\d+),\s*r(\d+)$", RegexOptions.IgnoreCase), Action = ADD},
                new Pattern {Regex = new Regex(@"^SUB\s+r(\d+),\s*r(\d+)$", RegexOptions.IgnoreCase), Action = SUB},
                new Pattern {Regex = new Regex(@"^BR\s+([A-Z_]\w*)$", RegexOptions.IgnoreCase), Action = BR},
                new Pattern {Regex = new Regex(@"^BRGZ\s+([A-Z_]\w*),\s*r(\d+)$", RegexOptions.IgnoreCase), Action = BRGZ},
                new Pattern {Regex = new Regex(@"^SYSCALL\s+(\d+)$", RegexOptions.IgnoreCase), Action = SYSCALL},
                new Pattern {Regex = new Regex(@"^CLEAR$", RegexOptions.IgnoreCase), Action = CLEAR},
                new Pattern {Regex = new Regex(@"^(\w+):$", RegexOptions.IgnoreCase), Action = (x, y) => { }},
                new Pattern {Regex = new Regex(@"^//\w*$", RegexOptions.IgnoreCase), Action = (x, y) => { }},
            };
        }
        
        public string Evaluate(string text)
        {
            string[] lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            LabelsRegistration(lines);
            int n = lines.Length;
            while (_state.Ip < n)
            {
                bool metka = false;
                foreach (var pattern in _patterns)
                {
                    Match m = pattern.Regex.Match(lines[_state.Ip].Trim());
                    if (m.Success)
                    {
                        string[] groups = m.Groups.OfType<Group>().Select(x => x.Value).ToArray();
                        pattern.Action(_state, groups);
                        metka = true;
                        break;
                    }
                }
                if(!metka)
                    throw new ParserException(lines[_state.Ip]);
                _state.Ip++;
            }
            return _syscall;
        }
        private void LabelsRegistration(string[] text)
        {
            Regex labelRegex = new Regex(@"^(\w+):$", RegexOptions.IgnoreCase);
            for (int i = 0; i < text.Length; i++)
            {
                Match m = labelRegex.Match(text[i]);
                if (m.Success)
                    _state.Labels.Add(m.Groups[1].Value ,i);
            }
        }

        private void RegCorrect(State s, string g)
        {
            if (int.Parse(g) > s.Reg.Length)
                throw new AddressException(s.Ip + 1, g);
        }

        private void LD(State s, string[] g)
        {
            if (int.Parse(g[2]) > 255)
                throw new ParameterOutOfRangeException(s.Ip + 1, g[2]);
            RegCorrect(s, g[1]);
            s.Reg[int.Parse(g[1])] = byte.Parse(g[2]);
        }
        private void MOV(State s, string[] g)
        {
            RegCorrect(s, g[1]);
            RegCorrect(s, g[2]);
            s.Reg[int.Parse(g[1])] = s.Reg[int.Parse(g[2])];
        }
        private void ADD(State s, string[] g)
        {
            RegCorrect(s, g[1]);
            RegCorrect(s, g[2]);
            s.Reg[int.Parse(g[1])] += s.Reg[int.Parse(g[2])];
        }
        private void SUB(State s, string[] g)
        {
            RegCorrect(s, g[1]);
            RegCorrect(s, g[2]);
            s.Reg[int.Parse(g[1])] -= s.Reg[int.Parse(g[2])];
        }
        private void BRGZ(State s, string[] g)
        {
            RegCorrect(s, g[2]);
            if (s.Reg[int.Parse(g[2])] > 0)
            {
                if (!s.Labels.ContainsKey(g[1]))
                    throw new LabelUnavailableException( s.Ip + 1, g[1]);
                s.Ip = s.Labels[g[1]];
            }
        }
        private void BR(State s, string[] g)
        {
            if (!s.Labels.ContainsKey(g[1]))
                throw new LabelUnavailableException(s.Ip + 1, g[1]);
            s.Ip = s.Labels[g[1]];
        }
        private void SYSCALL(State s, string[] g)
        {
            RegCorrect(s, g[1]);
            _syscall += string.Format("\nr{0} = {1}",g[1], s.Reg[int.Parse(g[1])]);
        }
        private void CLEAR(State s, string[] g)
        {
            s.Reg = s.Reg.Select(x => (byte)0).ToArray();
        }
    }
}