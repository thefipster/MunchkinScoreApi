using System;

namespace TheFipster.Munchkin.GameDomain
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
