using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApplication.Model;
using StudentApplication.Service;

namespace StudentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogInSevice _service;

        public LoginController(ILogInSevice sevice)
        {
            _service= sevice;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel model)
        {
            var token = await _service.SignUp(model);
            return Ok(token);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserModel model)
        {
            var token=await _service.LogInAsync(model);
            return Ok(token);
        }
    }
}
