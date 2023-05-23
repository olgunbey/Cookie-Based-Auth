using CookieBasedAuth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> LogInAsync(string username,string pass,bool DbRegister)
        {
            await _service.LogInAsync(pass, username,DbRegister);
            return Ok();
        }

        [Authorize(Policy = "PasswordPolicy")]
        [HttpGet("Kontrols")]
        public IActionResult Kontrols()
        {

            return Ok("giriş yapıldı");
        }
        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
           await _service.LogOutAsync();
            return Ok();
        }
        [HttpGet("AddRole")]
        public async Task<IActionResult> AddRole(string Rolename)
        {
            await _service.AddDbRoleAsync(Rolename);
            return Ok();
        }
        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(string username,string rolename)
        {
           await _service.UserRoleAsync(username, rolename);
            return Ok();
        }

    }
}
