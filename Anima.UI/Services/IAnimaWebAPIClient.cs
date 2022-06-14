using System.Threading.Tasks;
using Anima.UI.Models;

namespace Anima.UI.Services
{
    public interface IAnimaWebAPIClient
    {
        Task<AnimalsResponse> GetPetsForAdoption();
    }
}