using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class ClassMessage : GameModifierMessage
    {
        public Guid PlayerId { get; set; }
        public string Class { get; set; }
    }
}
