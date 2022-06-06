using System.Collections.Generic;
using Doggo.API.Models;
using Doggo.Web3.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Doggo.API.Data
{
    public class RescuesRepository : IRescuesRepository
    {
        private readonly AppDbContext _dbContext;
        public RescuesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AFA Create(AFA rescue)
        {
            var id = Guid.NewGuid().ToString().Replace("-", "");
            rescue.Id = id;

            _dbContext.AnimalsForAdoption.Add(rescue);
            _dbContext.SaveChanges();
            
            return rescue;
        }

        public void Delete(string id)
        {
            var animal = GetOne(id);
            if(animal == null) return;
            _dbContext.AnimalsForAdoption.Remove(animal);
            _dbContext.SaveChanges();
        }

        public IEnumerable<AFA> Filter(Filter filter)
        {
            return GetAll().Where(it => it.Species == filter.Species);
        }

        public AFA GetOne(string id)
        {
            return _dbContext.AnimalsForAdoption
                            .Where(a => a.Id == id)
                            .FirstOrDefault();
        } 

        public IEnumerable<AFA> GetAll()
        {
            return _dbContext.AnimalsForAdoption;
        }

        public AFA Update(AFA replacement)
        {
            throw new System.NotImplementedException();
        }
    }
}