using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public interface IGameStatePollService
    {
        GameStatePollRequest GetScoreRequest(Guid gameId);
        void FinishRequest(Guid gameId, Scoreboard score);
    }
}
