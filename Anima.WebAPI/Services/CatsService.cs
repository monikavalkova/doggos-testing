using System.Threading.Tasks;
using Anima.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anima.WebAPI.Services
{
    public class CatsService : ICatsService
    {
        public CatsResponse GetCatsOfBreed(string breed)
        {
            throw new System.NotImplementedException();
        }
    }
}