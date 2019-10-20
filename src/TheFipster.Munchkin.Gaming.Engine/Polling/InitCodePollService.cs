using System;
using TheFipster.Munchkin.Gaming.Polling;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.Gaming.Engine.Polling
{
    public class InitCodePollService : PollService<string, Guid>, IInitCodePollService
    {
        public InitCodePollService() : base(
            size: 100,
            expiration: TimeSpan.FromMinutes(30),
            scanFrequency: TimeSpan.FromMinutes(1))
        { }
    }
}
