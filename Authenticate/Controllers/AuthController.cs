using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authenticate.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel registerModel)
        {
            return Ok("Register");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            return Ok("Login");
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test(LoginModel loginModel)
        {
            return Ok("test");
        }

        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class RegisterModel
        {
            public string username { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
