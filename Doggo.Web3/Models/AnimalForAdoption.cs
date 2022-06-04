using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    //TODO validation 
    ///<summary>
    ///Used when registering an animal for adoption.
    ///</summary>
    public class AFARequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("age")]
        public string Age { get; set; }
        [JsonPropertyName("story")]
        public string Story { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}