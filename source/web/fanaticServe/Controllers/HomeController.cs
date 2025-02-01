using fanaticServe.Data;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace fanaticServe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly fanaticServeContext _context;

        public HomeController(ILogger<HomeController> logger, fanaticServeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //try
            //{
            //    var iddata = _context.abustract_albums.ToList();
            //    return new ObjectResult(iddata);
            //}
            //catch (Exception e)
            //{
            //    Console.Write(e.Message);
            //}
            return View();
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
