using System;
using System.Threading.Tasks;

namespace TheFipster.Munchkin.Polling
{
    public class PollRequest<TValue>
    {
        private TaskCompletionSource<bool> _taskCompletion;
        private TimeSpan _timeout;
        private TValue _value;

        public PollRequest() 
            : this(TimeSpan.FromMinutes(60)) { }

        public PollRequest(TimeSpan timeout)
        {
            _timeout = timeout;
            _taskCompletion = new TaskCompletionSource<bool>();
        }

        public void Notify(TValue value)
        {
            _value = value;
            _taskCompletion.SetResult(true);
        }

        public async Task<TValue> WaitAsync()
        {
            await Task.WhenAny(
                _taskCompletion.Task, 
                Task.Delay(_timeout));

            return _value;
        }
    }
}
