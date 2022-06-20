
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Anima.WebAPI.Models
{
    public enum Species
    {
        DEFAULT_NULL_VALUE, CAT, DOG, DUCK, BUG
    }
    public enum Gender
    {
        FEMALE, MALE
    }

    public class AFA
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Story { get; set; }
        public string City { get; set; }
        public Species Species { get; set; }
        public Gender Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string Remarks { get; set; }
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
    }
}