using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class StarMatrixController : Controller
{
    private readonly IFanaticServeContext _context;

    public StarMatrixController(IFanaticServeContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var service = new StarMatrixService(_context);
        return View(service.GetStarMatrix());
    }
}
