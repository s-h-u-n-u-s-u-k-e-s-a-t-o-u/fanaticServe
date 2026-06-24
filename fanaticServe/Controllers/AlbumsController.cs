using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class AlbumsController : Controller
{
    private readonly ILogger<AlbumsController> _logger;
    private readonly IAlbums _albums;

    public AlbumsController(ILogger<AlbumsController> logger, IAlbums albums)
    {
        _logger = logger;
        // DIでサービスを受け取る
        _albums = albums;
    }

    // GET: Albums
    [HttpGet]
    public IActionResult Index(string sortOrder, string searchString)
    {
        // 並び変えの判定
        ViewData["DateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["TitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        return View(this._albums.GetAllAlbums(sortOrder, searchString));
    }

    // GET: Albums/Details/5
    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var album = this._albums.GetDetailAlbum(id.Value);
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

        return View(this._albums.GetAlbumArticles(sortOrder, searchString));
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

        return View(this._albums.GetAlbumGroup(id.Value, sortOrder));
    }
}
