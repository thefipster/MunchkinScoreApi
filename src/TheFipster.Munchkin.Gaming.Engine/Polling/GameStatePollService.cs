using System;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Polling;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.Gaming.Engine.Polling
{
    public class GameStatePollService : PollService<Guid, Game>, IGameStatePollService
    {
        public GameStatePollService(): base(
            size: 100, 
            expiration: TimeSpan.FromMinutes(15), 
            scanFrequency: TimeSpan.FromSeconds(30))
        { }
    }
}
