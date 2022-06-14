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
    public class AFARequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("story")]
        public string Story { get; set; }

        //TODO add regex validation
        [Required(ErrorMessage = "Field contact_number must not be null.")]
        [JsonProperty(PropertyName = "contact_number")] //spec. to override Newtonsoft's default serialization options 
        [JsonPropertyName("contact_number")] 
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Field city must not be null.")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Field country must not be null.")]
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [Range(1, 4, ErrorMessage = "Field species must not be null.")]
        [JsonPropertyName("species")]       
        public Species Species { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }
    }
}