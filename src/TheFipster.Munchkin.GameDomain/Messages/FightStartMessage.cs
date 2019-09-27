using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class FightStartMessage : GameMessage
    {
        public Guid PlayerId { get; set; }
        public string Monster { get; set; }
        public string Reason { get; set; }
    }
}
