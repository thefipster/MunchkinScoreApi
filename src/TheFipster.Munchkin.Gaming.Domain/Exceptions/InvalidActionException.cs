using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException(string message)
            : base(message) { }
    }
}
