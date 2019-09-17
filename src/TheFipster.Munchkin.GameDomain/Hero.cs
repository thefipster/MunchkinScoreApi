using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class Hero
    {
        public Hero() { }

        public Hero(IPlayer player)
        {
            Player = player;
            Level = 1;
            Races = new List<string>();
            Classes = new List<string>();
        }

        public IPlayer Player { get; set; }
        public int Level { get; set; }
        public int Bonus { get; set; }
        public ICollection<string> Races { get; set; }
        public ICollection<string> Classes { get; set; }
    }
}
