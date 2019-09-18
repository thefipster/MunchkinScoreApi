using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class StartMessage : GameMessage
    {
        public StartMessage() { }

        public StartMessage(Guid gameId) : base(gameId) { }
    }
}
