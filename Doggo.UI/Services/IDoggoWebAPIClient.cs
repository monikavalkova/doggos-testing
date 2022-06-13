using Doggo.UI.Models;

namespace Doggo.UI.Services
{
    public interface IDoggoWebAPIClient
    {
        Task<AnimalsResponse> GetPetsForAdoption();
    }
}