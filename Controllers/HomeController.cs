using fanaticServe.Core.Models;
using fanaticServe.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace fanaticServe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly FanaticServeContext _context;

    public HomeController(ILogger<HomeController> logger, FanaticServeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("/Events/Articles");

        //        return View();
    }

    [HttpGet]
    public IActionResult ReleaseNote()
    {
        return View(fanaticServe.ReleaseNote.GetReleaseNotes());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
