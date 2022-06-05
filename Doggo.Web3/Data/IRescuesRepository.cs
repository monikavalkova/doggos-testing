using System.Collections.Generic;
using Doggo.API.Models;

namespace Doggo.API.Data
{
    public interface IRescuesRepository
    {
        AFA Find(string id);
        bool SaveChanges();
        IEnumerable<AFA> GetAll();

        IEnumerable<AFA> GetAllCats();
        IEnumerable<AFA> GetAllDogs();
        AFA Create(AFA newOne);
        void Delete(string id);
        AFA Update(AFA replacement);
    }
}