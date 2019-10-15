using System.Collections.Generic;

namespace TheFipster.Munchkin.StashRepository.Models
{
    public class Curse
    {
        public string Name { get; set; }

        public ICollection<string> Effects { get; set; }
    }
}
