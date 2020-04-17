using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace BugTrackerXUnitTest
{
    public class BasicControllerIntegrationTests
    : IClassFixture<WebApplicationFactory<BugTracker.Startup>>
    {
        private readonly WebApplicationFactory<BugTracker.Startup> _factory;

        public BasicControllerIntegrationTests(WebApplicationFactory<BugTracker.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("Home/Index")]
        [InlineData("Home/Privacy")]

        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("Projects/")]
        [InlineData("Projects/Edit/13")]
        [InlineData("Projects/Details/13")]
        [InlineData("Projects/Delete/13")]
        [InlineData("BugTracker/Bugs/Create")]
        [InlineData("BugTracker/Bugs/Edit/2")]
        [InlineData("BugTracker/Bugs/Details/2")]
        [InlineData("BugTracker/Bugs/Delete/2")]
        public async Task Get_EndpointsReturnUnauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);
            var actual = response.StatusCode;

            // Assert
            var expected = System.Net.HttpStatusCode.Unauthorized;
            Assert.Equal(expected, actual);
        }


    }
}