using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Anima.WebAPI.Models
{
    public class Filter
    {
        [JsonPropertyName("species")]
        [Required(ErrorMessage = "Species is a required field.")]
        public Species Species { get; set; }
    }
}