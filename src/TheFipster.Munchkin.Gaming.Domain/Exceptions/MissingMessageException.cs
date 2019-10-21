using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
{
    public class MissingMessageException : Exception
    {
        public MissingMessageException(string message)
            : base(message) { }
    }
}
