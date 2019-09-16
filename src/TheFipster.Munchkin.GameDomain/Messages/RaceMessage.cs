using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class RaceMessage : GameModifierMessage
    {
        public Guid PlayerId { get; set; }
        public string Race { get; set; }
    }
}
