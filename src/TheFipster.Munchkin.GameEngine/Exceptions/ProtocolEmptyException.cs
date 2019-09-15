using System;

namespace TheFipster.Munchkin.GameEngine.Exceptions
{
    public class ProtocolEmptyException : Exception
    {
        public ProtocolEmptyException(string message) : base(message) { }
    }
}
