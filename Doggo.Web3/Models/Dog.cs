using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    public class DogDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName(("name"))]
        public string Name { get; set; }
        [JsonPropertyName("temperament")]
        public string Temperament { get; set; } //e.g., "Aloof, Clownish, Dignified, Independent, Happy"
        [JsonPropertyName("bred_for")]
        public string BredFor { get; set; } //e.g., "Hunting bears"
        [JsonPropertyName("origin")]
        public string Origin { get; set; } //e.g., "Afghanistan, Iran, Pakistan"
        
    }
}