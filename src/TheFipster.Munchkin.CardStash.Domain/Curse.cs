using System.Collections.Generic;

namespace TheFipster.Munchkin.CardStash.Domain
{
    public class Curse : Card
    {
        public ICollection<string> Effects { get; set; }
    }
}
