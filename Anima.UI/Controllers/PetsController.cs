using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Anima.UI.Models;
using Anima.UI.Services;
using System.Threading.Tasks;

namespace Anima.UI.Controllers

{
    public class PetsController : Controller
    {
        private readonly IAnimaWebAPIClient _client;
        public PetsController(IAnimaWebAPIClient client) => _client = client;

        public async Task<IActionResult> Index()
        {
            //TODO catch unexpected exceptions + add test
            var petsForAdoption = await _client.GetPetsForAdoption();
            return View();
        }
    }
}