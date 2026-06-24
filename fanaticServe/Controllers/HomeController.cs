using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace fanaticServe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IEvents _events;

    public HomeController(ILogger<HomeController> logger, IEvents events)
    {
        _logger = logger;
        _events = events;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int limit = 3;

        // Dashboard‚Ìƒf[ƒ^‚ğæ“¾‚µ‚ÄView‚É“n‚·
        DashBoard dashboardData = new DashBoard();

        dashboardData.RecentLiveEvents.AddRange(_events.GetRecentLiveEvent(limit));
        dashboardData.RecentlyChangedEvents.AddRange(_events.GetRecentlyChangedEvents(limit));

        return View(dashboardData);
    }

    [HttpGet]
    public IActionResult ReleaseNote()
    {
        return View(fanaticServe.Dto.ReleaseNote.GetReleaseNotes());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
