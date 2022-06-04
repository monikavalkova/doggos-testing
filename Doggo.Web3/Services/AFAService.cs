using System.Threading.Tasks;
using Doggo.API.Data;
using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Services
{
    public class AFAService : IAFAService
    {
        private IRescuesRepository _repo;
        public AFAService(IRescuesRepository repo)
        {
            _repo = repo;
        }

        public AFAResponse Add(AFARequest animal)
        { 
            //TODO add an in-memory database
            throw new System.NotImplementedException();
        }

        public string Ping()
        {
            return "OK, good to go.";
        }
    }
}