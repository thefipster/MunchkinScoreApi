using System;

namespace TheFipster.Munchkin.Gaming.Domain
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid();
        }

        public Player(string name, string gender) : this()
        {
            Name = name;
            Gender = gender;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
