using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Anima.UI.Models
{
    public class AnimalsResponse
    {
        [JsonPropertyName("animalsForAdoption")]
        public IEnumerable<AnimalResponse> Animals { get; set; }
    }
}