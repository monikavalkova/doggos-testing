using Microsoft.AspNetCore.Mvc;

namespace Doggo.API.Controllers
{
    public class DogsController : ControllerBase
    {
        private IDogsService _service;
        public DogsController(IDogsService service)
        {
            _service = service;
        }
    }
}