using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class SongsController : Controller
{
    private readonly ISongs _songs;

    public SongsController(ISongs songs)
    {
        _songs = songs;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder, string searchString)
    {
        ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
        ViewData["CountSort"] = sortOrder == "Count" ? "Count_desc" : "Count";

        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        return View(_songs.GetAllSongs(sortOrder, searchString));
    }

    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View(_songs.GetSong(id.Value));
    }
}
