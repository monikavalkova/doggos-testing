using Doggo.API.Models;

namespace Doggo.API.Services
{
    public interface IDogsService
    {
        public DogsResponse GetDogsOfBreed(string breed);

    }
}