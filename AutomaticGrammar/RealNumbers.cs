using System;
using System.Linq;

namespace AutomaticGrammar
{
    public class RealNumbers
    {
        private readonly int[,] _table;
        private readonly int[] _final;

        public RealNumbers()
        {
            _table = new[,]{
                {2,1,7,7,7},
                {2,7,7,7,7},
                {2,7,3,4,7},
                {3,7,7,4,7},
                {6,5,7,7,7},
                {6,7,7,7,7},
                {6,7,7,7,7},
                {7,7,7,7,7},
            };
            _final = new[] { 2, 3, 6 };
        }

        private int ch(char c)
        {
            if (char.IsDigit(c))
                return 0;
            if (c == '+' || c == '-')
                return 1;
            if (c == '.' || c == ',')
                return 2;
            if (c == 'e' || c == 'E')
                return 3;
            return 4;
        }

        public bool Vetify(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException();
            int status = text.Aggregate(0, (current, c) => _table[current, ch(c)]);
            return Array.IndexOf(_final, status) != -1;
        }
    }
}
