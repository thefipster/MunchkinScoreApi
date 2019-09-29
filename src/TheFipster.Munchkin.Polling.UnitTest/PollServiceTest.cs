using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TheFipster.Munchkin.Polling.UnitTest
{
    public class PollServiceTest
    {
        private string someRequestId = "This is a request";

        [Fact]
        public void CheckUnknownRequest_ResultsInFalseReturn_Test()
        {
            // Arrange
            var service = new PollService<string, Guid>(
                1,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMilliseconds(100));

            // Act & Assert
            Assert.False(service.CheckRequest(someRequestId));
        }

        [Fact]
        public void CheckExistingRequest_ResultsInTrueReturn_Test()
        {
            // Arrange
            var service = new PollService<string, Guid>(
                1,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMilliseconds(100));

            // Act
            service.StartRequest(someRequestId);

            // Assert
            Assert.True(service.CheckRequest(someRequestId));
        }

        [Fact]
        public void CheckFinishedRequest_ResultsInFalseReturn_Test()
        {
            // Arrange
            var service = new PollService<string, Guid>(
                1,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMilliseconds(100));

            // Act
            service.StartRequest(someRequestId);
            service.FinishRequest(someRequestId, Guid.NewGuid());

            // Assert
            Assert.False(service.CheckRequest(someRequestId));
        }

        [Fact]
        public void StartingSameRequestTwice_ResultsInSameRequest_Test()
        {
            // Arrange
            var service = new PollService<string, Guid>(
                1,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromMilliseconds(100));

            // Act
            var request1 = service.StartRequest(someRequestId);
            var request2 = service.StartRequest(someRequestId);

            // Assert
            Assert.Equal(request1, request2);
        }
    }
}
