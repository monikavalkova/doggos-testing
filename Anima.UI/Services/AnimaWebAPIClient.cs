using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Anima.UI.Models;

namespace Anima.UI.Services
{
    public class AnimaWebAPIClient : IAnimaWebAPIClient
    {
        private static readonly string WEB_API_ADDRESS_IN_CONFIGURATION = "WebApi";

        public async Task<AnimalsResponse> GetPetsForAdoption()
        {
            var client = getHttpClient();
            var url = "https://localhost:5001/api/animals";  //TODO move to appsettings.json //Add dependency of the configurations!! https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0#jcp
            var animalResp = await client.GetFromJsonAsync<AnimalsResponse>(url);
            return animalResp;
        }

        //private readonly IConfiguration _configuration;

        //public DoggoWebAPIClient(IConfiguration configuration) 
        //    => _configuration = configuration; //TODO add to IOC container

        //private string GetBaseUrl()
        // => _configuration[WEB_API_ADDRESS_IN_CONFIGURATION];


        private HttpClient getHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            return client; //TODO improve - use existing instance instead of creating a new one every time 
        }
    }
}