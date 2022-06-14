using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Anima.WebAPI.Models
{
    //TODO validation 
    ///<summary>
    ///Used when registering an animal for adoption.
    ///</summary>
    public class AFAPatchRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("story")]
        public string Story { get; set; }

        //TODO add regex validation
        [JsonProperty(PropertyName = "contact_number")] //spec. to override Newtonsoft's default serialization options 
        [JsonPropertyName("contact_number")] 
        public string ContactNumber { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }
    }
}