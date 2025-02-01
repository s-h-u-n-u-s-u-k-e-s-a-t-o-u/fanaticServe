using fanaticServe.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers
{
    public class SongsController : Controller
    {
        private readonly fanaticServeContext _context;

        public SongsController(fanaticServeContext context) => _context = context;

        public IActionResult Index()
        {
            var songs = _context.songs.ToList();
            return View(songs);
        }
    }
}
