using System.Text.Json.Serialization;
using Doggo.UI.Models;

namespace Doggo.UI.Models
{
    public class AnimalsResponse
    {
        [JsonPropertyName("animalsForAdoption")]
        public IEnumerable<AnimalResp> Animals { get; set; }
    }
}