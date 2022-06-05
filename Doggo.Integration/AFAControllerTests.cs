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
using Doggo.API.Models;

namespace Doggo.Integration
{
    public class AFAControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "/api/rescues";
        private const string ID_OF_A_RESCUE = "b770d25a-89dc-4eg6-a322-dfa4532354f9";


        public AFAControllerTests(WebApplicationFactory<Startup> fixture)
        =>  _client = fixture.CreateClient(); 
        
        
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

        [Fact]
        public async Task getOne_with_non_existent_id_returns_404()
        {
            //act
            var response = await _client.GetAsync(BASE_URL + "/non-existent-id");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task getOne_should_return_one()
        {
            //act
            var response = await _client.GetAsync(BASE_URL + "/existing-id");
                        
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
           
            var stringifiedResponse = await response.Content.ReadAsStringAsync();
            var resultAfa = JsonConvert.DeserializeObject<AFAResponse>(stringifiedResponse);
            
            resultAfa.Species.Should().Be(Species.CAT);
            resultAfa.Name.Should().Be("Cassandra");            
        }
    }
}
