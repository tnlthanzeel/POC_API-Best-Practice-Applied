using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebService.Service;

namespace WebService.Controllers
{
    [Route("user")]
    public class HomeController : Controller
    {
        public IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            return Ok(await _userService.GetAll());
        }
    }
}
