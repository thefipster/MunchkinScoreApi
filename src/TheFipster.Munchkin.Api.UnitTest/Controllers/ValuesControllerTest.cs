using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Api.Controllers;
using Xunit;

namespace TheFipster.Munchkin.Api.UnitTest.Controllers
{
    public class ValuesControllerTest
    {
        [Fact]
        public void Get_WhenCalled_ReturnsOkAndValues()
        {
            // Arrange
            var controller = new ValuesController();

            // Act
            var getResult = controller.Get();

            // Assert
            var okResult = Assert.IsAssignableFrom<OkObjectResult>(getResult.Result);
            var data = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
            Assert.Equal(2, data.Count());
        }
    }
}
