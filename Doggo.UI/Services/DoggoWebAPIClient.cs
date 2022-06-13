using System.Net.Http.Headers;
using System.Net.Mime;
using Doggo.UI.Models;

namespace Doggo.UI.Services
{
    public class DoggoWebAPIClient : IDoggoWebAPIClient
    {
        private static readonly string WEB_API_ADDRESS_IN_CONFIGURATION = "WebApiDoggo";
       
        public Task<AnimalsResponse> GetPetsForAdoption()
        {
            var client = getHttpClient();
            var url = "https://localhost:5001/api/rescues";  //TODO make sure it works! THEN move to appsettings.json
            return null;                                     //Add dependency towards the configurations!!
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