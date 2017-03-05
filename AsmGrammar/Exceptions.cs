using System;

namespace AsmGrammar
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message){}
    }

    public class AddressException : Exception
    {
        public AddressException(string message) : base(message){}
    }

    public class LabelUnavailableException : Exception
    {
        public LabelUnavailableException(string message) : base(message) { }
    }

    public class ParameterOutOfRangeException : Exception
    {
        public ParameterOutOfRangeException(string message) : base(message) { }
    }
}
