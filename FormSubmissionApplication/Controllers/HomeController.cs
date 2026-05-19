using System.Diagnostics;
using FormSubmissionApplication.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FormSubmissionApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserNameModel model)
        {
            string json = JsonSerializer.Serialize(model);
            System.IO.File.WriteAllText("user.json", json);

            return View();
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
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
