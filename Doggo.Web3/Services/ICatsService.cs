using System.Threading.Tasks;
using Doggo.API.Models;

namespace Doggo.API.Services
{
    /// <summary>
    /// Class <c>Point</c> models a point in a two-dimensional plane.
    /// </summary>
    public interface ICatsService
    {
        /// <summary>
        /// Method <c>GetCatsByBreed</c> returns a information about various
        /// cat breeds, the name of which match the incomplete breed string.
        /// </summary>
        /// <param name="breed">an incomplete breed name, e.g., sphy for sphynx</param>
        /// <remarks>
        /// Calls an external API "api.thecatapi.com"
        /// </remarks>
        /// <returns>
        /// CatsResponse
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Thrown when no cats were found.
        /// </exception>
        CatsResponse GetCatsOfBreed(string breed);
        
        /// <summary>
        /// Method <c>Ping</c> is used for testing purposes.
        /// </summary>
        Task<object> Ping();
    }
}