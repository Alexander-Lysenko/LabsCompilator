using System;
using System.Collections.Generic;

namespace GrammarOfArithmetic
{
    public class ExpressionModule
    {
        private readonly string _expression;
        private int _position;
        private readonly Stack<double> _stack;

        public ExpressionModule(string expression)
        {
            _expression = expression.Trim().Replace(" ", string.Empty);
            _position = 0;
            _stack = new Stack<double>();
        }

        public double Parse()
        {
            E();
            if (_position != _expression.Length)
                throw new GrammarException(String.Format("{0}", _position));
            return _stack.Pop();
        }

        private bool CharCorrect(char c)
        {
            if (_position == _expression.Length)
                return false;
            return _expression[_position] == c;
        }

        private bool StringCorrect(string s)
        {
            if (_position == _expression.Length)
                return false;
            foreach (var c in s)
            {
                if (!CharCorrect(c))
                    return false;
                _position++;
            }
            return true;
        }

        private void E()
        {
            T();
            Ep();
        }
        private void T()
        {
            F();
            Tp();
        }

        private void Ep()
        {
            if (CharCorrect('+'))
            {
                EpF((x, y) => x + y);
            }
            else if (CharCorrect('-'))
            {
                EpF((x, y) => x - y);
            }
        }

        private void Tp()
        {
            if (CharCorrect('*'))
            {
                TpF((x, y) => x * y);
            }
            else if (CharCorrect('/'))
            {
                TpF((x, y) => x / y);
            }
            else if (CharCorrect('^'))
            {
                TpF((x, y) => (int)Math.Pow(x, y));
            }
        }

        private void EpF(Func<double, double, double> f)
        {
            _position++;
            T();
            Ep();
            double a = _stack.Pop();
            double b = _stack.Pop();
            _stack.Push(f(b, a));
        }

        private void TpF(Func<double, double, double> f)
        {
            _position++;
            F();
            Tp();
            _stack.Push(f(_stack.Pop(), _stack.Pop()));
        }

        private void F()
        {
            string sign = "";
            if (CharCorrect('-') || CharCorrect('+'))
            {
                sign += _expression[_position];
                _position++;
            }
            if (CharCorrect('('))
            {
                Fp();
                if (sign == "-")
                    _stack.Push(_stack.Pop() * -1);
                return;
            }
            if (StringCorrect("SQRT("))
            {
                Fp();
                _stack.Push(Math.Sqrt(_stack.Pop()) * (sign == "-" ? -1 : 1));
                return;
            }
            string text = sign;
            while (_position < _expression.Length && (char.IsDigit(_expression[_position]) || CharCorrect('.')))
            {
                text += _expression[_position];
                _position++;
            }
            try
            {
                if (!string.IsNullOrEmpty(text))
                    _stack.Push(double.Parse(text));
            }
            catch (Exception)
            {
                throw new GrammarException(String.Format("{0}", _position));
            }
        }

        private void Fp()
        {
            _position++;
            E();
            if (!CharCorrect(')'))
                throw new GrammarException(String.Format("{0}", _position));
            _position++;
        }
    }
}