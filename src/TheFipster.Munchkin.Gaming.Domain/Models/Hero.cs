using System.Collections.Generic;

namespace TheFipster.Munchkin.Gaming.Domain
{
    public class Hero
    {
        public Hero() { }

        public Hero(Player player)
        {
            Player = player;
            Level = 1;
            Races = new List<string>();
            Classes = new List<string>();
        }

        public Player Player { get; set; }
        public int Level { get; set; }
        public int Bonus { get; set; }
        public ICollection<string> Races { get; set; }
        public ICollection<string> Classes { get; set; }
    }
}
