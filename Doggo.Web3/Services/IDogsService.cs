using System.Threading.Tasks;
using Doggo.API.Models;

namespace Doggo.API.Services
{
    public interface IDogsService
    {
        DogsResponse GetDogsOfBreed(string breed);
        Task<object> Ping();
    }
}