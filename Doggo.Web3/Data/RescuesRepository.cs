using System.Collections.Generic;
using Doggo.API.Models;
using Doggo.Web3.Data;
using System.Linq;

namespace Doggo.API.Data
{
    public class RescuesRepository : IRescuesRepository
    {
        private readonly AppDbContext _dbContext;
        public RescuesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AFA Create(AFA newOne)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public AFA Find(string id)
        {
            return _dbContext.AnimalsForAdoption
                            .Where(a => a.Id == id)
                            .FirstOrDefault();
        } 

        public IEnumerable<AFA> GetAll()
        {
            return _dbContext.AnimalsForAdoption;
        }

        public IEnumerable<AFA> GetAllCats()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AFA> GetAllDogs()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public AFA Update(AFA replacement)
        {
            throw new System.NotImplementedException();
        }
    }
}