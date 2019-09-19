using System;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public interface IInitCodePollService
    {
        InitCodePollRequest GetWaitHandle(string initCode);
        void CreateWaitHandle(string initCode);
        void FinishCodePollRequest(string initCode, Guid gameId);
    }
}
