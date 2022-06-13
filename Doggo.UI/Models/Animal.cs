using System.Text.Json.Serialization;

namespace Doggo.UI.Models
{
    public class AnimalResp
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }
    }
}