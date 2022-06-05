using System.Collections.Generic;
using Doggo.API.Models;

namespace Doggo.API.Data
{
    public interface IRescuesRepository
    {
        AFA GetOne(string id);
        IEnumerable<AFA> GetAll();
        IEnumerable<AFA> Filter(Filter filter);
        AFA Create(AFA newOne);
        void Delete(string id);
        AFA Update(AFA replacement);
    }
}