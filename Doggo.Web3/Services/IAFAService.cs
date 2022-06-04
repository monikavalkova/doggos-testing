using System.Threading.Tasks;
using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Services
{
    //TODO add documentation
    public interface IAFAService
    {
        AFAResponse Add(AFARequest animal);

        /// <summary>
        /// Method <c>Ping</c> is used for testing purposes.
        /// </summary>
        string Ping();
    }
}