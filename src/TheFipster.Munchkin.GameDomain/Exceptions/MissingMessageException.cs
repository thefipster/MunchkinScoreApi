using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class MissingMessageException : Exception
    {
        public MissingMessageException(string message)
            : base(message) { }
    }
}
