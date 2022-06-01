using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Services
{
    public interface IDogsService
    {
        public DogsResponse GetDog(string breed);

    }
}