using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LibAsm
{
    struct Sequence
    {
        public string r1;
        public string r2;
        public Action<string, string> a;
    }

    public class Assembler
    {
        private readonly Dictionary<Regex, object> _regexes;
        private readonly List<Sequence> _sequences;
        private readonly Hashtable _registers;
        public Assembler()
        {
            _registers = new Hashtable();
            Clear();
            _regexes = new Dictionary<Regex, object>();
            _sequences = new List<Sequence>();
            _regexes.Add(new Regex(@"^LD\s+r(\d{1, 2}),\s*#(\d{1, 3})$", RegexOptions.IgnoreCase), (Action<string, string>)LD);
            _regexes.Add(new Regex(@"^MOV\s+r(\d{1, 2}),\s*r(\d{1, 2})$", RegexOptions.IgnoreCase), (Action<string, string>)MOV);
            _regexes.Add(new Regex(@"^ADD\s+r(\d{1, 2}),\s*r(\d{1, 2})$", RegexOptions.IgnoreCase), (Action<string, string>)ADD);
            _regexes.Add(new Regex(@"^SUB\s+r(\d{1, 2}),\s*r(\d{1, 2})$", RegexOptions.IgnoreCase), (Action<string, string>)SUB);
            _regexes.Add(new Regex(@"^BR\s+([A-Z _]\w*)$", RegexOptions.IgnoreCase), null);
            _regexes.Add(new Regex(@"^BRGZ\s+([A-Z _]\w*),\s*r(\d{1, 2})$", RegexOptions.IgnoreCase), (Func<string, bool>)BRGZ);
            _regexes.Add(new Regex(@"^SYSCALL\s+\d{1, 2}$", RegexOptions.IgnoreCase), (Func<int, int>)SYSCALL);
            _regexes.Add(new Regex(@"^CLEAR$", RegexOptions.IgnoreCase), (Action)Clear);
            //_regexes.Add(new Regex(@"^\w+:$", RegexOptions.IgnoreCase), null);
        }

        public string Parser(string[] array)
        {
            Regex labelRegex = new Regex(@"^\w+:$", RegexOptions.IgnoreCase);
            string label = "";

            return "";
        }

        private void Clear()
        {
            _regexes.Clear();
            for (int i = 0; i < 16; i++)
            {
                _registers.Add("r" + i, 0);
            }
        }

        private void LD(string r, string value)
        {
            if (int.Parse(value) > 255)
                throw new ArgumentOutOfRangeException();
            if (!_registers.ContainsKey(r))
                throw new Exception("Используется несуществующий аддрес");
            _registers[r] = byte.Parse(value);
        }

        private void MOV(string r1, string r2)
        {
            if (!_registers.ContainsKey(r1) || !_registers.ContainsKey(r2))
                throw new Exception("Используется несуществующий аддрес");
            _registers[r1] = _registers[r2];
        }

        private void ADD(string r1, string r2)
        {
            if (!_registers.ContainsKey(r1) || !_registers.ContainsKey(r2))
                throw new Exception("Используется несуществующий аддрес");
            _registers[r1] = (byte)((byte)_registers[r1] + (byte)_registers[r2]);
        }

        private void SUB(string r1, string r2)
        {
            if (!_registers.ContainsKey(r1) || !_registers.ContainsKey(r2))
                throw new Exception("Используется несуществующий аддрес");
            _registers[r1] = (byte)((byte)_registers[r1] - (byte)_registers[r2]);
        }

        private int SYSCALL(int n)
        {
            if (n > 15 || n < 0)
                throw new ArgumentOutOfRangeException();
            return (byte)_registers["r" + n];
        }

        private bool BRGZ(string r)
        {
            if (!_registers.ContainsKey(r))
                throw new Exception("Используется несуществующий аддрес");
            return (byte)_registers[r] > 0;
        }

    }
}
