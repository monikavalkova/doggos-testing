using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    // TODO add documentation
    //i.e., Animal For Adoption
    public class AFAResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("species")]
        public Species Species { get; set; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
    }
}