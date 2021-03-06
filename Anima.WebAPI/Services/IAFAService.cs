using System.Collections.Generic;
using System.Threading.Tasks;
using Anima.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anima.WebAPI.Services
{
    //TODO add documentation
    public interface IAFAService
    {
        AFAResponse Add(AFARequest animal);

        /// <summary>
        /// Method <c>Ping</c> is used for testing purposes.
        /// </summary>
        string Ping();

        Task<AFAResponse> GetOne(string id);

        Task<IEnumerable<AFAResponse>> GetAll();

        Task<IEnumerable<AFAResponse>> Filter(Filter filter);

        Task Delete(string id);

        Task<AFAResponse> Replace(string id, AFARequest animal);

        Task<AFAResponse> PartialUpdate(string id, AFAPatchRequest animal);
    }
}