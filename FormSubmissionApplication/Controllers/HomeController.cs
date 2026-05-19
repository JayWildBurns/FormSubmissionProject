using System.Diagnostics;
using FormSubmissionApplication.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FormSubmissionApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserNameModel model)
        {
            string json = JsonSerializer.Serialize(model);

            //Names File to include Date and name
            string fileFirstName = CreateSafeFileName(model.FirstName);
            string fileLastName = CreateSafeFileName(model.LastName);
            string filename = $"{fileFirstName}_{fileLastName}_{DateTime.Now:yyyyMMdd_HHmmss}.json";

            //Save to 'Users' folder
            string folder = Path.Combine(_env.ContentRootPath, "Users");
            Directory.CreateDirectory(folder);
            string fileSavePath = Path.Combine(folder, filename);

            //Writes File.
            System.IO.File.WriteAllText(fileSavePath, json);

            return View();
        }

        // Function: CreateSafeFileName
        // Takes a string, replaces invalid file name characters with '_'.
        private string CreateSafeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
