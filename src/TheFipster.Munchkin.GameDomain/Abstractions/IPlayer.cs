using System;

namespace TheFipster.Munchkin.GameDomain
{
    public interface IPlayer
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Gender { get; set; }
    }
}
