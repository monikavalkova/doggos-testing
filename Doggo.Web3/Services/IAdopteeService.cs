using Doggo.API.Models;

namespace Doggo.API.Services
{
    //TODO add documentation
    public interface IAdopteeService
    {
        //TODO add documentation for the response
         MessageDto AddForAdoption(AnimalForAdoption animal);
    }
}