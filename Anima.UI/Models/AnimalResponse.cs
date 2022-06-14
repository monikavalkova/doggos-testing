using System.Text.Json.Serialization;

namespace Anima.UI.Models
{
    public class AnimalResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }
    }
}