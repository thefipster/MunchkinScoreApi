using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class Fight
    {
        public Guid Id { get; set; }
        public List<Guid> Players { get; set; }
        public List<string> Monsters { get; set; }
    }
}
