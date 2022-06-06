using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    // TODO add documentation
    //i.e., Animal For Adoption
    public class AFAResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("species")]
        public string Species { get; set; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        public string ContactNumber { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }

        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("story")]
        public string Story { get; set; }
    }
}