using System.Text.Json.Serialization;

namespace Doggo.API.Models
{
    // TODO add documentation
    //i.e., Animal For Adoption
    public class AFAResponse
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}