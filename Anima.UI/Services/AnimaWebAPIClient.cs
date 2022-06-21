using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Anima.UI.Models;
using Microsoft.Extensions.Configuration;

namespace Anima.UI.Services
{
    public class AnimaWebAPIClient : IAnimaWebAPIClient
    {
        private readonly HttpClient _httpClient;
        private static readonly string WEB_API_ADDRESS_IN_CONFIGURATION = "WebApi";

        public async Task<AnimalsResponse> GetPetsForAdoption()
        {
            var url = _configuration[WEB_API_ADDRESS_IN_CONFIGURATION] + "/animals";
            var animalResp = await _httpClient.GetFromJsonAsync<AnimalsResponse>(url);
            return animalResp;
        }

        private readonly IConfiguration _configuration;

        public AnimaWebAPIClient(IConfiguration configuration, HttpClient httpClient) 
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        private string GetBaseUrl()
        => _configuration[WEB_API_ADDRESS_IN_CONFIGURATION];
    }
}