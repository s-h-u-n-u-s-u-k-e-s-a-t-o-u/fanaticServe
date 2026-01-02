using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace fanaticServe.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IFanaticServeContext _context;

    public HomeController(ILogger<HomeController> logger, IFanaticServeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int limit = 3;

        // DashboardÇÃÉfÅ[É^ÇéÊìæÇµÇƒViewÇ…ìnÇ∑
        DashBoard dashboardData = new DashBoard();

        var service = new fanaticServe.Back.EventService(_context);

        dashboardData.RecentLiveEvents.AddRange(service.GetRecentLiveEvent(limit));
        dashboardData.RecentlyChangedEvents.AddRange(service.GetRecentlyChangedEvents(limit));

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
