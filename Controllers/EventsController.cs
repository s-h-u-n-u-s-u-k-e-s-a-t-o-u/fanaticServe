using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class EventsController : Controller
{
    private readonly IFanaticServeContext _context;

    public EventsController(IFanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder)
    {
        // ソート条件の初期化
        ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["TitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";

        return View(new EventService(_context).GetAllEvents(sortOrder));
    }

    // GET: Events/Details/5
    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View(new EventService(_context).GetDetailEvent(id.Value));
    }

    [HttpGet]
    public IActionResult EventGroup(Guid? id, string sortOrder)
    {
        if (id == null)
        {
            return NotFound();
        }

        // ソート条件の初期化
        ViewData["GroupDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["GroupTitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";

        return View(new EventService(_context).GetEventGroup(id.Value, sortOrder));
    }

    [HttpGet]
    public IActionResult Articles(string sortOrder, string searchString)
    {
        // ソート条件の初期化
        ViewData["ArticleDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["ArticleTitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        return View(new EventService(_context).GetAllEventArticles(sortOrder, searchString));
    }
}
