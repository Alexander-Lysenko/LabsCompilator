using System;

namespace MdLiteGrammar
{
    public class KeyOrValueException : Exception
    {
        public KeyOrValueException(string message) : base(message) { }
    }

    public class NotKeyException : Exception
    {
        public NotKeyException(string message) : base(message) { }
    }

}