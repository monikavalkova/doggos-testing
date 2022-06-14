using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Anima.WebAPI.Models
{
    // TODO add documentation
    public class AnimalsResponse
    {
        [JsonPropertyName("animalsForAdoption")]
        public IEnumerable<AFAResponse> AnimalsForAdoption { get; set; }
    }
}