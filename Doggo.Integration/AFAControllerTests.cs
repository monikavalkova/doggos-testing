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
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Doggo.Integration
{
    public class AFAControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "/api/rescues";
        private const string CATS_URL = BASE_URL + "/cats";
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
            var response = await _client.GetAsync(BASE_URL + "/cass-id");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
           
            var stringifiedResponse = await response.Content.ReadAsStringAsync();
            var resultAfa = JsonConvert.DeserializeObject<AFAResponse>(stringifiedResponse);
            
            resultAfa.Species.Should().Be(Species.CAT);
            resultAfa.Name.Should().Be("Cassandra");            
        }

        [Fact]
        public async Task getAll_should_return_all()
        {
            //act
            var response = await _client.GetAsync(BASE_URL);
            //asert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentAsString = await StringifyContent(response);
            var result =  Deserialize<IEnumerable<AFAResponse>>(contentAsString);

            result.Count().Should().Be(5);
        }

        [Fact(Skip = "for now")]
        public async Task getAllCats_should_return_2_cats()
        {
            //act
            var httpResponse = await _client.GetAsync(CATS_URL);

            var contentAsString = await httpResponse.Content.ReadAsStringAsync();
            var contentAsEnumerable = JsonConvert.DeserializeObject<IEnumerable<AFAResponse>>(contentAsString);

            contentAsEnumerable.Count().Should().Be(2);

        }

        private T Deserialize<T>(string stringifiedResponse)
        {
            return JsonConvert.DeserializeObject<T>(stringifiedResponse);
        }

        private async Task<string> StringifyContent(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}
