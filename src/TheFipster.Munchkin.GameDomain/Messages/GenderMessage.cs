using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class GenderMessage : GameMessage
    {
        public Guid PlayerId { get; set; }
        public string Gender { get; set; }
    }
}
