using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class ClassMessage : GameSwitchMessage<string>
    {
        public ClassMessage() { }

        public Guid PlayerId { get; set; }
    }
}
