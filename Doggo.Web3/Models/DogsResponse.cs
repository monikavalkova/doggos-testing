using System.Collections.Generic;

namespace Doggo.API.Models
{
    public class DogsResponse
    {
        public IEnumerable<DogDto> Dogs { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}