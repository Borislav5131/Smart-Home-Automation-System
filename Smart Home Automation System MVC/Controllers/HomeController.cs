namespace Smart_Home_Automation_System_MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smart_Home_Automation_System_MVC.Attributes;
    using Smart_Home_Automation_System_MVC.Models;
    using System.Diagnostics;

    [RedirectToLogin]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}