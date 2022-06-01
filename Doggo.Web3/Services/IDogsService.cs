using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Services
{
    public interface IDogsService
    {
        public DogsResponse GetDogsOfBreed(string breed);

    }
}