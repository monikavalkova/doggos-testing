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
        private static readonly string WEB_API_ADDRESS_IN_CONFIGURATION = "WebApi";

        public async Task<AnimalsResponse> GetPetsForAdoption()
        {
            var client = getHttpClient();
            var url = _configuration[WEB_API_ADDRESS_IN_CONFIGURATION] + "/animals";
            var animalResp = await client.GetFromJsonAsync<AnimalsResponse>(url);
            return animalResp;
        }

        private readonly IConfiguration _configuration;

        public AnimaWebAPIClient(IConfiguration configuration) 
           => _configuration = configuration;

        private string GetBaseUrl()
        => _configuration[WEB_API_ADDRESS_IN_CONFIGURATION];


        private HttpClient getHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            return client; //TODO improve - use existing instance instead of creating a new one every time 
        }
    }
}