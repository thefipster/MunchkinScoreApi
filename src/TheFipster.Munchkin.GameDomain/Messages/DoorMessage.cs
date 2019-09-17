using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class DoorMessage : GameMessage
    {
        public Guid PlayerId { get; set; }
    }
}
