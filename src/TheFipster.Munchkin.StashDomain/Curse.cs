using System.Collections.Generic;

namespace TheFipster.Munchkin.StashDomain
{
    public class Curse : Card
    {
        public ICollection<string> Effects { get; set; }
    }
}
