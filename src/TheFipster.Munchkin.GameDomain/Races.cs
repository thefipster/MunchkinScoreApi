using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public static class Races
    {
        public static IEnumerable<string> Items => new[] { "Mensch", "Elf", "Gnom", "Halbling", "Echsenmensch" };
    }
}
