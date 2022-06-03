namespace Doggo.API.Models
{
    public class CatDto
    {
        public string Name { get; set; }
        public string LifeSpan { get; set; }
        public int Adaptability { get; set; }
        public string Description { get; set; }
        public string CfaUrl { get; set; } 

        public string ImageUrl { get; set; }
    }
}