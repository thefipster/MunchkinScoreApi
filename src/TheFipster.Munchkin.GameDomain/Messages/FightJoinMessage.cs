using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class FightJoinMessage : GameMessage
    {
        public Guid PlayerId { get; set; }
    }
}
