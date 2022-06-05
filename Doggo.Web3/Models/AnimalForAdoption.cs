
using System.ComponentModel.DataAnnotations;

namespace Doggo.API.Models
{
    public enum Species
    {
        CAT, DOG, DUCK
    }
    public class AFA
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Story { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public Species Species { get; set; }
    }
}