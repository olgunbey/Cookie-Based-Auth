using CookieBasedAuth.Service;
using Microsoft.AspNetCore.Mvc;

namespace CookieBasedAuth.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(string username,string password)
        {
          await  _service.SignInAsync(password, username);
            return Ok();
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInAsync(string username,string pass)
        {
            await _service.LogInAsync(pass, username);
            return Ok();
        }
    }
}
