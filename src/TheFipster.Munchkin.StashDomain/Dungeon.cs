using System.Collections.Generic;

namespace TheFipster.Munchkin.StashDomain
{
    public class Dungeon
    {
        public string Name { get; set; }

        public ICollection<string> Effects { get; set; }
    }
}
