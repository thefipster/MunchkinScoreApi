using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class Scoreboard
    {
        public Scoreboard()
        {
            Dungeons = new List<string>();
            Heroes = new List<Hero>();
        }

        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
        public ICollection<string> Dungeons { get; set; }
        public ICollection<Hero> Heroes { get; set; }
        public Fight Fight { get; set; }
    }
}
