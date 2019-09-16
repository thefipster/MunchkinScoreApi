using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class EndMessage : GameMessage
    {
        public DateTime End { get; set; }
    }
}
