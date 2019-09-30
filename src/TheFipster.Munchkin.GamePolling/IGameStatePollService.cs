using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.GamePolling
{
    public interface IGameStatePollService 
        : IPollService<Guid, Game>
    { }
}
