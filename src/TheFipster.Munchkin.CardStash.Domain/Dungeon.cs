using System.Collections.Generic;

namespace TheFipster.Munchkin.CardStash.Domain
{
    public class Dungeon : Card
    {
        public ICollection<string> Effects { get; set; }
    }
}
