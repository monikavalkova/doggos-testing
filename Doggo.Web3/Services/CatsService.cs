using System.Threading.Tasks;
using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Services
{
    public class CatsService : ICatsService
    {
        public CatsResponse GetCatsOfBreed(string breed)
        {
            throw new System.NotImplementedException();
        }
    }
}