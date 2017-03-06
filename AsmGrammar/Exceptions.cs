using System;

namespace AsmGrammar
{
    public class ParserException : Exception
    {
        public ParserException(string message) :
            base("Строка не распознана: " + message)
        {
        }
    }

    public class AddressException : Exception
    {
        public AddressException(int ip, string g) :
            base(string.Format("Строка {0}: Используется несуществующий адрес: {1}", ip, g))
        {
        }
    }

    public class LabelUnavailableException : Exception
    {
        public LabelUnavailableException(int ip, string g) :
            base(string.Format("Строка {0}: Попытка перейти на несуществующую метку: {1}", ip, g))
        {
        }
    }

    public class ParameterOutOfRangeException : Exception
    {
        public ParameterOutOfRangeException(int ip, string g) :
            base(string.Format("Строка {0}: Заданный аргумент ({1}) находится вне диапазона допустимых значений", ip, g))
        {
        }
    }
}
