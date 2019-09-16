using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException(string message)
            : base(message) { }
    }
}
