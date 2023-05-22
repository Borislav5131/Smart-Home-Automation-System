namespace Smart_Home_Automation_System_API.Controllers
{
    using ApplicationService.DTOs.User;
    using ApplicationService.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        
        private readonly IUserService userService;

        public AuthController(IUserService userService, IConfiguration configuration = null)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var result = await userService.Register(registerUserDTO);

            if (result == false)
            {
                return BadRequest("Can not registered user!");
            }

            return Ok("User registered successfully!");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var login = await userService.Login(loginUserDTO);

            if (!login)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = await userService.GenerateJwtToken(loginUserDTO);

            return Ok(new { token });
        }
    }
}
