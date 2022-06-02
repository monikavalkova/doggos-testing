using System;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Doggo.Web3;
using static System.Net.HttpStatusCode;
namespace Doggo.Integration
{
    public class DogsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public DogsControllerIntegrationTests(WebApplicationFactory<Startup> fixture)
        {    _client = fixture.CreateClient(); }
        
        [Fact]
        public async Task test_ping()
        {
            //arrange
            var url = "https://localhost/ping";
            //act
            var result = await _client.GetAsync(url);
            //assert
            result.StatusCode.Should().Be(OK);
        }
    }
}
