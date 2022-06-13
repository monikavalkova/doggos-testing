using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Doggo.UI.Models;
using Doggo.UI.Services;

namespace Doggo.UI.Controllers;

public class PetsController : Controller
{
    private readonly IDoggoWebAPIClient _client;
    public PetsController(IDoggoWebAPIClient client) => _client = client;

    public async Task<IActionResult> Index()
    {
        //TODO catch unexpected exceptions + add test
        var petsForAdoption = await _client.GetPetsForAdoption();
        return View();
    }
}