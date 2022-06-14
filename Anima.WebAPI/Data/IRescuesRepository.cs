using System.Collections.Generic;
using Anima.WebAPI.Models;

namespace Anima.WebAPI.Data
{
    public interface IRescuesRepository
    {
        AFA GetOne(string id);
        IEnumerable<AFA> GetAll();
        IEnumerable<AFA> Filter(Filter filter);
        AFA Create(AFA newOne);
        void Delete(string id);
        AFA UpdatePartial(AFA dbEntity);
    }
}