using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class GameModifierMessage : GameMessage
    {
        public Modifier Modifier { get; set; }
    }
}
