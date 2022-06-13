using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    // TODO add documentation
    //i.e., Animals For Adoption
    public class AnimalsResponse
    {
        public IEnumerable<AFAResponse> animalsForAdoption { get; set; }
    }
}