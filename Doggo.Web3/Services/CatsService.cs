using System.Threading.Tasks;
using Doggo.API.Models;

namespace Doggo.API.Services
{
    public class CatsService : ICatsService
    {
        public CatsResponse GetCatsOfBreed(string breed)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> Ping()
        {
            return new {Text = "OK. Good to go."};
        }
    }
}