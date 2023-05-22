namespace Smart_Home_Automation_System_MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smart_Home_Automation_System_MVC.Interfaces;
    using Smart_Home_Automation_System_MVC.Models.User;

    public class AuthController : Controller
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel loginUserModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUserModel);
            }

            var result = await userService.Login(loginUserModel);

            if (result != null)
            {
                Response.Cookies.Append("token", result);
                return RedirectToAction("Index", "Home");
            }

            return View(loginUserModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel registerUserModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerUserModel);
            }

            var result = await userService.Register(registerUserModel);

            if (result == true)
            {
                return RedirectToAction("Login");
            }

            return View(registerUserModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies.TryGetValue("token", out var token))
            {
                Response.Cookies.Delete("token");
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}
