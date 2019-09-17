using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class EndMessage : GameMessage
    {
        public EndMessage() { }

        public EndMessage(Guid gameId) : base(gameId) { }
    }
}
