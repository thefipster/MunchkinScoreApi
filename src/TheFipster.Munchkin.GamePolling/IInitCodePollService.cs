using System;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.GamePolling
{
    public interface IInitCodePollService
        : IPollService<string, Guid>
    { }
}
