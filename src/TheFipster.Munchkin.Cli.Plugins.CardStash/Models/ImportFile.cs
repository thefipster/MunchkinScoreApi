using System.Collections.Generic;
using TheFipster.Munchkin.CardStash.Domain;

namespace TheFipster.Munchkin.Cli.Plugins.CardStash.Models
{
    public class ImportFile
    {
        public IEnumerable<Monster> Monsters { get; set; }
        public IEnumerable<Curse> Curses { get; set; }
        public IEnumerable<Dungeon> Dungeons { get; set; }
    }
}
