using System;
using System.Threading.Tasks;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public abstract class PollRequest<T>
    {
        private TaskCompletionSource<bool> _taskCompletion;
        private TimeSpan _timeout;
        private T _value;

        public PollRequest() 
            : this(TimeSpan.FromMinutes(60)) { }

        public PollRequest(TimeSpan timeout)
        {
            _timeout = timeout;
            _taskCompletion = new TaskCompletionSource<bool>();
        }

        public void Notify(T value)
        {
            _value = value;
            _taskCompletion.SetResult(true);
        }

        public async Task<T> WaitAsync()
        {
            await Task.WhenAny(
                _taskCompletion.Task, 
                Task.Delay(_timeout));

            return _value;
        }
    }
}
