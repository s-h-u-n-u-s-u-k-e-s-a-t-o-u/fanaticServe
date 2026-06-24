using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class EventsController : Controller
{
    private readonly ILogger<EventsController> _logger;
    private readonly IEvents _events;

    public EventsController(ILogger<EventsController> logger, IEvents events)
    {
        _logger = logger;
        _events = events;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder)
    {
        // ソート条件の初期化
        ViewData["DateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["TitleSortParam"] = sortOrder == "title" ? "title_desc" : "title";

        return View(_events.GetAllEvents(sortOrder));
    }

    // GET: Events/Details/5
    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View(_events.GetDetailEvent(id.Value));
    }

    [HttpGet]
    public IActionResult EventGroup(Guid? id, string sortOrder)
    {
        if (id == null)
        {
            return NotFound();
        }

        // ソート条件の初期化
        ViewData["GroupDateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["GroupTitleSortParam"] = sortOrder == "title" ? "title_desc" : "title";

        return View(_events.GetEventGroup(id.Value, sortOrder));
    }

    [HttpGet]
    public IActionResult Articles(string sortOrder, string searchString)
    {
        // ソート条件の初期化
        ViewData["ArticleDateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["ArticleTitleSortParam"] = sortOrder == "title" ? "title_desc" : "title";
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        return View(_events.GetAllEventArticles(sortOrder, searchString));
    }
}
