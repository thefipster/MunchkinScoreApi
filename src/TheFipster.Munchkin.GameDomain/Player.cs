using System;

namespace TheFipster.Munchkin.GameDomain
{
    public class Player : IPlayer
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
