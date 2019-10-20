using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
{
    public class MissingActionException : Exception
    {
        public MissingActionException(string message) : base(message) { }
    }
}
