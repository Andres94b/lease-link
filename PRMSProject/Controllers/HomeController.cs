using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMSProject.Models;

namespace PRMSProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrmsdatabaseContext _context;


        public HomeController(ILogger<HomeController> logger, PrmsdatabaseContext context )
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var listings = _context.Listings.Include(l => l.Apartment).ToList();

            return View(listings);
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
