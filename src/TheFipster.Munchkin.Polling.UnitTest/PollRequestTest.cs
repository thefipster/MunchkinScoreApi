using Xunit;

namespace TheFipster.Munchkin.Polling.UnitTest
{
    public class PollRequestTest
    {
        [Fact]
        public void CreatePollRequest_ResultsInWaitStillRunning_Test()
        {
            var request = new PollRequest<string>();
            var task = request.WaitAsync();

            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void CreatePollRequestAndNotifyIt_ResultsInWaitTaskBeingCompleted_Test()
        {
            var expectedValue = "this is a message";
            var request = new PollRequest<string>();
            var task = request.WaitAsync();
            request.Notify(expectedValue);

            Assert.True(task.IsCompletedSuccessfully);
        }
    }
}
