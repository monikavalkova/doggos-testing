using System.Collections.Generic;

namespace Anima.WebAPI.Models
{
    public class CatsResponse
    {
        public IEnumerable<CatDto> Cats { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}