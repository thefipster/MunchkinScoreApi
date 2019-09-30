using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class MissingActionException : Exception
    {
        public MissingActionException(string message) : base(message) { }
    }
}
