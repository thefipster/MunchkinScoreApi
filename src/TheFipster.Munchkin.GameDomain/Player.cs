using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid();
            Level = 1;
            Races = new List<string>();
            Classes = new List<string>();
        }

        public Player(string name, string gender) : this()
        {
            Name = name;
            Gender = gender;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Level { get; set; }
        public int Bonus { get; set; }
        public ICollection<string> Races { get; set; }
        public ICollection<string> Classes { get; set; }
    }
}
