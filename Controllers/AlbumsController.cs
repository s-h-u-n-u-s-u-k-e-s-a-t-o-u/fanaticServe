using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class AlbumsController : Controller
{
    private readonly IFanaticServeContext _context;

    public AlbumsController(IFanaticServeContext context)
    {
        _context = context;
    }

    // GET: Albums
    [HttpGet]
    public IActionResult Index(string sortOrder, string searchString)
    {
        // 並び変えの判定
        ViewData["DateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["TitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        return View(new AlbumService(_context).GetAllAlbums(sortOrder, searchString));
    }

    // GET: Albums/Details/5
    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = new AlbumService(_context).GetDetailAlbum(id.Value);
        if (album == null)
        {
            return NotFound();
        }
        return View(album);
    }

    [HttpGet]
    public IActionResult Articles(string sortOrder, string searchString)
    {
        // 並び変えの判定
        ViewData["ArticleDateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["ArticleTitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        return View(new AlbumService(_context).GetAlbumArticles(sortOrder,  searchString));
    }

    [HttpGet]
    public IActionResult AlbumGroup(Guid? id, string sortOrder)
    {
        if (id == null)
        {
            return NotFound();
        }

        // 並び変えの判定
        ViewData["GroupDateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["GroupTitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        return View(new AlbumService(_context).GetAlbumGroup(id.Value));
    }    
}
