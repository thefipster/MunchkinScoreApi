using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
{
    public class GameIdHeaderException : Exception
    {
        public GameIdHeaderException() { }

        public GameIdHeaderException(string message) : base(message) { }
    }
}
