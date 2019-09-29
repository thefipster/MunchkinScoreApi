using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePolling;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.GameEngine.Polling
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
