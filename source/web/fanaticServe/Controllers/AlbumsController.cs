using fanaticServe.Data;
using fanaticServe.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace fanaticServe.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly fanaticServeContext _context;

        public AlbumsController(fanaticServeContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            /// 一覧表形式でアルバム情報を表示する
            var records =
                from absal in _context.Abstract_albums
                join lk in _context.Abstract_album_link on absal.abstract_album_id equals lk.abstract_album_id
                join alb in _context.albums on lk.album_id equals alb.album_id
                join media in _context.media.DefaultIfEmpty() on alb.media_type equals media.media_type
                orderby alb.release_on
                select new ShowableAlbum()
                {
                    abstract_album_id = absal.abstract_album_id,
                    title = absal.title,
                    album_id = alb.album_id,
                    detailTitle = alb.title,
                    release_on = alb.release_on,
                    media = media.name ?? ""
                }
                ;

            return View(await records.ToListAsync());
        }

        public IActionResult Articles()
        {
            var articles =
                        _context.Abstract_albums
                       .Select(absalb => new Abstract_album()
                       {
                           abstract_album_id = absalb.abstract_album_id,
                           title = absalb.title,
                           created_at = absalb.created_at,
                           modified_at = absalb.modified_at
                       }).ToList()
                       ;

            foreach (var article in articles)
            {
                var albums = from lk in _context.Abstract_album_link
                             where lk.abstract_album_id == article.abstract_album_id
                             join alb in _context.albums on lk.album_id equals alb.album_id
                             select new Album()
                             {
                                 album_id = alb.album_id,
                                 title = alb.title,
                                 code = alb.code,
                                 release_on = alb.release_on,
                                 label_id = alb.label_id,
                                 media_type = alb.media_type
                             };
                article.albums = albums.ToList();
            }

            return View(articles);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.albums
                .FirstOrDefaultAsync(m => m.album_id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        private bool abstract_albumExists(Guid id)
        {
            return _context.Abstract_albums.Any(e => e.abstract_album_id == id);
        }
    }
}
