using System;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Doggo.Web3;
using static System.Net.HttpStatusCode;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Doggo.Integration
{
    public class AFAControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "/api/rescues";
        private const string ID_OF_A_RESCUE = "b770d25a-89dc-4eg6-a322-dfa4532354f9";


        public AFAControllerIntegrationTests(WebApplicationFactory<Startup> fixture)
        {    
            _client = fixture.CreateClient(); 
        }
        
        [Fact]
        public async Task test_ping()
        {
            //arrange
            var url = "/ping";
            //act
            var result = await _client.GetAsync(url);
            //assert
            result.StatusCode.Should().Be(OK);
            //result.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var jsonResponse = await result.Content.ReadAsStringAsync();
            var stringResponse = JsonConvert.DeserializeObject<string>(jsonResponse);
            
            stringResponse.Should().Be("OK, good to go.");
        }

        [Fact(Skip = "for now")]
        public async Task getOne_with_wrong_url_returns_404()
        {
            //todo
            
        }




    }
}
