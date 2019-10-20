using System;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.Gaming.Polling
{
    public interface IGameStatePollService 
        : IPollService<Guid, Game>
    { }
}
