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
using System.Text;

namespace Doggo.Integration
{
    public class AFAControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "/api/rescues";
        private const string FILTER_URL = BASE_URL + "/limit";
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
            
            resultAfa.Species.Should().Be("CAT");
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

        [Fact]
        public async Task filter_should_return_2_cats()
        {
            //act
            var filter = new Filter(){Species = Species.CAT};
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await _client.PostAsync(FILTER_URL, requestBodyJson);
            //assert
            var contentAsString = await httpResponse.Content.ReadAsStringAsync();
            var contentAsEnumerable = JsonConvert.DeserializeObject<IEnumerable<AFAResponse>>(contentAsString);

            contentAsEnumerable.Count().Should().Be(2);
        }

        [Fact]
        public async Task filter_should_return_2_dogs()
        {
            //act
            var filter = new Filter(){Species = Species.DOG};
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await _client.PostAsync(FILTER_URL, requestBodyJson);
            //assert
            var contentAsString = await httpResponse.Content.ReadAsStringAsync();
            var contentAsEnumerable = JsonConvert.DeserializeObject<IEnumerable<AFAResponse>>(contentAsString);

            contentAsEnumerable.Count().Should().Be(2);
        }

        [Fact]
        public async Task filter_should_return_1_duck()
        {
            //act
            var filter = new Filter(){Species = Species.DUCK};
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await _client.PostAsync(FILTER_URL, requestBodyJson);
            //assert
            var contentAsString = await httpResponse.Content.ReadAsStringAsync();
            var contentAsEnumerable = JsonConvert.DeserializeObject<IEnumerable<AFAResponse>>(contentAsString);

            contentAsEnumerable.Count().Should().Be(1);
            contentAsEnumerable.First().Name.Should().Be("Donald");
        }

        [Fact]
        public async Task filter_should_return_404_when_the_bug_dies()
        {
            //act
            var deleteResponse = await _client.DeleteAsync(BASE_URL + "/bug-id");

            var filter = new Filter(){Species = Species.BUG};
            var jsonRequestBody = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await _client.PostAsync(FILTER_URL, jsonRequestBody);
            //assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(Skip = "until Post endpoint is ready")]
        public async Task delete_animal_should_delete_indeed()
        {
            ////arrange
            //var newBug = new AFARequest(){ Name = "Poco", Species = Species.BUG };
            //var jsonRequestBody = new StringContent(JsonConvert.SerializeObject(newBug), Encoding.UTF8, MediaTypeNames.Application.Json);
            //var postResponse = await _client.PostAsync(BASE_URL, jsonRequestBody);

            //act
            var responseBeforeDeletion = await _client.GetAsync(BASE_URL + "");
            await _client.DeleteAsync(BASE_URL + "");
            var responseAfterDeletion = await _client.GetAsync(BASE_URL + "");
            
            //assert
            responseBeforeDeletion.StatusCode.Should().Be(HttpStatusCode.OK);
            responseAfterDeletion.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Theory]
        [InlineData("name", "age", "story", null, "city", "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "contactNum", null, "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "contactNum", "city", null, Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "contactNum", "city", "country", Gender.FEMALE, null)]
        public async Task post_must_validate(string name, string age, string story, 
                                            string contactNumber, string city, string country, 
                                            Gender gender, Species species)
        {
            //arrange
            var requestBody = new AFARequest(){Name = name, Age = age, Story = story, 
                            ContactNumber = contactNumber, City = city, Country = country, 
                            Gender = gender, Species = species };

            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act
            var httpResponse = await _client.PostAsync(BASE_URL, requestBodyJson);
            //assert
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            jsonResponse.Should().Contain("must not be null.");
        }

        [Theory]
        [InlineData("name", "age", "story", "contactNum", "city", "country", Gender.MALE, Species.DOG)]
        public async Task post_with_all_required_fields_must_pass_validations(string name, string age, 
                                            string story, string contactNumber, string city,
                                            string country, Gender gender, Species species)
        {
            //arrange
            var requestBody = new AFARequest(){Name = name, Age = age, Story = story, 
                            ContactNumber = contactNumber, City = city, Country = country, 
                            Gender = gender, Species = species };
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act
            var httpResponse = await _client.PostAsync(BASE_URL, requestBodyJson);
            //assert
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task post_returns_uri_in_location_header()
        {
            //arrange
            var requestBody = new AFARequest(){Name = "Maja", Age = "3", Story = "found on the street", 
                            ContactNumber = "+359884939914", City = "Sofia", Country = "Bulgaria", 
                            Gender = Gender.FEMALE, Species = Species.CAT, Remarks = "Excellent temperament. Will make a good pet." };
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act 
            var response = await _client.PostAsync(BASE_URL, requestBodyJson);
            //assert
            var animalForAdoption = JsonConvert.DeserializeObject<AFAResponse>(await response.Content.ReadAsStringAsync());
            response.Headers.Location.PathAndQuery.ToLowerInvariant().Should().Contain(BASE_URL.ToLowerInvariant());
            response.Headers.Location.PathAndQuery.Should().Contain(animalForAdoption.Id);
            //revert db changes
            _client.DeleteAsync(BASE_URL + "/" + animalForAdoption.Id);
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
