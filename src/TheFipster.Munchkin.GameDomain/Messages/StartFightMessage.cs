using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class StartFightMessage : GameMessage
    {
        public Guid PlayerId { get; set; }
        public string Monster { get; set; }
    }
}
