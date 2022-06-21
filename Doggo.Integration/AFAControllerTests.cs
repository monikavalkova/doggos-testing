using System;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

using static System.Net.HttpStatusCode;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Anima.WebAPI;
using Anima.WebAPI.Models;

namespace Doggo.Integration
{
    public class AFAControllerTests : IClassFixture<WebApplicationFactory<Startup>> //TODO refactor
    {
        private readonly HttpClient _client;
        private const string BASE_URL = "/api/animals";
        private const string FILTER_URL = BASE_URL + "/filter";

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
            resultAfa.Name.Should().Be("Cassi");            
        }

        [Fact]
        public async Task getAll_should_return_all()
        {
            //act
            var response = await _client.GetAsync(BASE_URL);
            
            //asert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentAsString = await StringifyContent(response);
            var result =  Deserialize<AnimalsResponse>(contentAsString);

            result.AnimalsForAdoption.Count().Should().Be(6);
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
            var contentAsEnumerable = JsonConvert.DeserializeObject<AnimalsResponse>(contentAsString);

            contentAsEnumerable.AnimalsForAdoption.Count().Should().Be(2);
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
            var contentAsEnumerable = JsonConvert.DeserializeObject<AnimalsResponse>(contentAsString);

            contentAsEnumerable.AnimalsForAdoption.Count().Should().Be(2);
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
            var contentAsEnumerable = JsonConvert.DeserializeObject<AnimalsResponse>(contentAsString);

            contentAsEnumerable.AnimalsForAdoption.Count().Should().Be(1);
            contentAsEnumerable.AnimalsForAdoption.First().Name.Should().Be("Donald");
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

        [Fact]
        public async Task delete_animal_should_delete_indeed()
        {
            //arrange
            var newBug = new AFARequest(){Name = "Jorko", Age = "1", Story = "rescued from a spider web", 
                            ContactNumber = "c", City = "c", Country = "c", Species = Species.BUG };

            var jsonRequestBody = new StringContent(JsonConvert.SerializeObject(newBug), Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await _client.PostAsync(BASE_URL, jsonRequestBody);
            var jorkoUri = postResponse.Headers.Location.PathAndQuery;
            //act
            var responseBeforeDeletion = await _client.GetAsync(jorkoUri);
            await _client.DeleteAsync(jorkoUri);
            var responseAfterDeletion = await _client.GetAsync(jorkoUri);
            
            //assert
            responseBeforeDeletion.StatusCode.Should().Be(HttpStatusCode.OK);
            responseAfterDeletion.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Theory]
        [InlineData("name", "age", "story", null, "city", "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", null, "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", "city", null, Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", "city", "country", Gender.FEMALE, null)]
        public async Task post_must_accept_only_valid_request_body(string name, string age, string story, 
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
            await _client.DeleteAsync(BASE_URL + "/" + animalForAdoption.Id);
        }

        [Fact]
        public async Task put_must_return_404_when_id_does_not_exist()
        {
            //arrange
            var requestBody = new AFARequest(){Name = "Maja", Age = "3", Story = "found on the street", 
                            ContactNumber = "+359884939914", City = "Sofia", Country = "Bulgaria", 
                            Gender = Gender.FEMALE, Species = Species.CAT, Remarks = "Excellent temperament. Will make a good pet." };
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act 
            var response = await _client.PutAsync(BASE_URL + "/" + "non-existent-id", requestBodyJson);
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("name", "age", "story", null, "city", "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", null, "country", Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", "city", null, Gender.FEMALE, Species.DOG)]
        [InlineData("name", "age", "story", "phone", "city", "country", Gender.FEMALE, null)]
        public async Task put_must_accept_only_valid_request_body(string name, string age, string story, 
                                            string contactNumber, string city, string country, 
                                            Gender gender, Species species)
        {
            //arrange
            var requestBody = new AFARequest(){Name = name, Age = age, Story = story, 
                            ContactNumber = contactNumber, City = city, Country = country, 
                            Gender = gender, Species = species };

            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act
            var httpResponse = await _client.PutAsync(BASE_URL + "/" + "cass-id", requestBodyJson);
            //assert
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            jsonResponse.Should().Contain("must not be null.");
        }

        [Fact]
        public async Task put_must_replace_the_whole_object()
        {
            //arrange
            var getResponse = _client.GetAsync(BASE_URL + "/" + "cass-id");

            var name = "Maja";
            var age = ">3";
            var story = "found on the street";
            var contactNumber = "+359884939914";
            var city = "Sofia";
            var country = "Bulgaria";
            var gender = Gender.FEMALE;
            var species = Species.CAT;
            var remarks = "Excellent temperament. Will make a good pet.";
            
            var requestBody = new AFARequest(){Name = name, Age = age, Story = story, 
                            ContactNumber = contactNumber, City = city, Country = country, 
                            Gender = gender, Species = species, Remarks = remarks };
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            
            //act 
            var response = await _client.PutAsync(BASE_URL + "/" + "cass-id", requestBodyJson);
            
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var afa = JsonConvert.DeserializeObject<AFAResponse>(await response.Content.ReadAsStringAsync());
            
            afa.Name.Should().Be(name);
            afa.Age.Should().Be(age);
            afa.Story.Should().Be(story);
            afa.ContactNumber.Should().Be(contactNumber);
            afa.City.Should().Be(city);
            afa.Country.Should().Be(country);
            afa.Gender.Should().Be(gender.ToString());
            afa.Species.Should().Be(species.ToString());
            afa.Remarks.Should().Be(remarks);
        }

        [Fact]
        public async Task patch_must_return_404_when_id_does_not_exist()
        {
            //arrange
            var requestBody = new AFAPatchRequest(){Story = "Roki found a loving home.", City = "Stockholm", Country = "Sweden"};
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act 
            var response = await _client.PatchAsync(BASE_URL + "/" + "non-existent-id", requestBodyJson);
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task patch_must_partially_update()
        {
            //arrange
            var requestBody = new AFAPatchRequest(){Story = "Roki found a loving home.", City = "Stockholm", Country = "Sweden"};
            var requestBodyJson = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, MediaTypeNames.Application.Json);
            //act 
            var patchResponse = await _client.PatchAsync(BASE_URL + "/" + "roki-id", requestBodyJson);
            //assert
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var animal = JsonConvert.DeserializeObject<AFAResponse>(await patchResponse.Content.ReadAsStringAsync());

            animal.Name.Should().Be("Roki");
            animal.Species.Should().Be("DOG");
            animal.Gender.Should().Be("FEMALE");
            animal.Story.Should().Be("Roki found a loving home.");
            animal.City.Should().Be("Stockholm");
            animal.Country.Should().Be("Sweden");
        }

        private T Deserialize<T>(string stringifiedResponse)
        => JsonConvert.DeserializeObject<T>(stringifiedResponse);

        private async Task<string> StringifyContent(HttpResponseMessage response)
        => await response.Content.ReadAsStringAsync();
        
    }
}
