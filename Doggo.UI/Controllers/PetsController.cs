using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Doggo.UI.Models;
using Doggo.UI.Services;

namespace Doggo.UI.Controllers;

public class PetsController : Controller
{
    private readonly IDoggoWebAPIClient _client;
    public PetsController(IDoggoWebAPIClient client) => _client = client;

    public IActionResult Index()
    {
        //TODO add tests
        return View();
    }
}