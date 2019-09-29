using System;

namespace TheFipster.Munchkin.GameEngine.Exceptions
{
    public class MissingActionException : Exception
    {
        public MissingActionException(string message) : base(message) { }
    }
}
