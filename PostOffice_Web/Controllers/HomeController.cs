using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostOffice.Models;
using PostOffice.Models.Database;

namespace PostOffice.Controllers
{
    public class HomeController(PostOfficeContext databaseContext, ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        [AllowAnonymous]
        public IActionResult Index()
        {
            var user = databaseContext.UserAccounts.FirstOrDefault(item => item.Id.ToString() == User.Identity.Name);

			return View((databaseContext.Publications.ToList(), user));
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
