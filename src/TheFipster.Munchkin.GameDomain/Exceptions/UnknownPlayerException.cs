using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class UnknownPlayerException : Exception
    {
        public UnknownPlayerException(string message) : base(message) { }

        public UnknownPlayerException(Guid playerId)
            : base($"Player with id='{playerId}' is unknown in the current game.") { }

        public UnknownPlayerException(string message, Exception exception) : base(message, exception) { }
    }
}
