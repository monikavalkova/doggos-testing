using System.Text.Json.Serialization;

namespace Doggo.UI.Models
{
    public class Animal
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}