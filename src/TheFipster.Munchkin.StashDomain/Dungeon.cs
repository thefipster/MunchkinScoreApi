using System.Collections.Generic;

namespace TheFipster.Munchkin.StashDomain
{
    public class Dungeon : Card
    {
        public ICollection<string> Effects { get; set; }
    }
}
