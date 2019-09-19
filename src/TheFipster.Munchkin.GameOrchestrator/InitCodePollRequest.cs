using System;
using System.Threading.Tasks;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public class InitCodePollRequest
    {
        private TaskCompletionSource<bool> _taskCompletion;
        private Guid? _gameId;

        public InitCodePollRequest()
        {
            _taskCompletion = new TaskCompletionSource<bool>();
        }

        public void Notify(Guid gameId)
        {
            _gameId = gameId;
            _taskCompletion.SetResult(true);
        }

        public async Task<Guid?> WaitAsync()
        {
            await Task.WhenAny(_taskCompletion.Task, Task.Delay(60000));
            return _gameId;
        }
    }
}
