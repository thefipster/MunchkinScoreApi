using System;
using TheFipster.Munchkin.Polling;

namespace TheFipster.Munchkin.Gaming.Polling
{
    public interface IInitCodePollService
        : IPollService<string, Guid>
    { }
}
