using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class GameIdHeaderException : Exception
    {
        public GameIdHeaderException() { }

        public GameIdHeaderException(string message) : base(message) { }
    }
}
