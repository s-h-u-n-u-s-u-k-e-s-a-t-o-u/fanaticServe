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
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            /// 一覧表形式でアルバム情報を表示する
            var records =
                from absal in _context.Abstract_albums
                join lk in _context.Abstract_album_link on absal.abstract_album_id equals lk.abstract_album_id
                join alb in _context.Albums on lk.album_id equals alb.album_id
                join media in _context.MediaTypes.DefaultIfEmpty() on alb.media_type equals media.media_type
                orderby alb.release_on
                select new ShowableAlbum()
                {
                    Abstract_album_id = absal.abstract_album_id,
                    Title = absal.title,
                    Album_id = alb.album_id,
                    DetailTitle = alb.title,
                    Release_on = alb.release_on,
                    Media = media.name ?? ""
                };

            return View(await records.ToListAsync());
        }

        [HttpGet]
        public IActionResult Articles()
        {
            var subTbl2 =
                from link in _context.Abstract_album_link
                join album in _context.Albums on link.album_id equals album.album_id
                group new { link, album } by link.abstract_album_id into tbl2
                select new { abstract_album_id = tbl2.Key, release_on = tbl2.Min(m => m.album.release_on) }
                       ;

            // 記事として表示する整形済みの抽象アルバム
            var articles = (
                from abs in _context.Abstract_albums
                join subT in subTbl2 on abs.abstract_album_id equals subT.abstract_album_id
                orderby subT.release_on
                select new ArticleAlbum()
                {
                    Abstract_album_id = abs.abstract_album_id,
                    Title = abs.title,
                    Release_On = subT.release_on
                }).ToList();

            // 抽象アルバムとアルバムを紐づけ
            foreach (var article in articles)
            {
                var albums = from lk in _context.Abstract_album_link
                             join alb in _context.Albums on lk.album_id equals alb.album_id
                             join label in _context.Labels.DefaultIfEmpty() on alb.label_id equals label.label_id
                             join media in _context.MediaTypes.DefaultIfEmpty() on alb.media_type equals media.media_type
                             where article.Abstract_album_id.Equals(lk.abstract_album_id)
                             orderby alb.release_on
                             select new DetailAlbum()
                             {
                                 Album_id = alb.album_id,
                                 Title = alb.title,
                                 Code = alb.code,
                                 Release_on = alb.release_on,
                                 Label = label.name ?? "",
                                 Media = media.name ?? ""
                             };
                article.Albums = albums.ToList();

                // アルバムにトラック情報を紐づけ
                foreach (var album in article.Albums)
                {
                    album.Tracks = GetTracks(album.Album_id);
                }

            }

            return View(articles);
        }

        // GET: Albums/Details/5
        [HttpGet]
        public IActionResult Detail(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = GetDetailAlbum(id.Value);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpGet]
        public IActionResult AlbumGroup(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(GetArticleAlbum(id.Value));
        }


        private DetailAlbum? GetDetailAlbum(Guid album_id)
        {
            var album = (from lk in _context.Abstract_album_link
                         join alb in _context.Albums on lk.album_id equals alb.album_id
                         join label in _context.Labels.DefaultIfEmpty() on alb.label_id equals label.label_id
                         join media in _context.MediaTypes.DefaultIfEmpty() on alb.media_type equals media.media_type
                         where album_id.Equals(lk.album_id)
                         orderby alb.release_on
                         select new DetailAlbum()
                         {
                             Album_id = alb.album_id,
                             Title = alb.title,
                             Code = alb.code,
                             Release_on = alb.release_on,
                             Label = label.name ?? "",
                             Media = media.name ?? ""
                         }).FirstOrDefault();

            if (album != null)
            {
                // アルバムにトラック情報を紐づけ                
                album.Tracks = GetTracks(album.Album_id);
            }
            return album;
        }

        private List<Track> GetTracks(Guid album_id)
        {
            var tracks =
                from trk in _context.Tracks
                where album_id.Equals(trk.album_id)
                orderby trk.track_no
                select new Track()
                {
                    track_id = trk.track_id,
                    track_no = trk.track_no,
                    title = trk.title,
                    length = trk.length,
                    song_id = trk.song_id
                };
            return tracks.ToList();
        }

        private ArticleAlbum? GetArticleAlbum(Guid id)
        {
            var subTbl2 =

                    (from link in _context.Abstract_album_link
                     join album in _context.Albums on link.album_id equals album.album_id
                     group new { link, album } by link.abstract_album_id into tbl2
                     select new { abstract_album_id = tbl2.Key, release_on = tbl2.Min(m => m.album.release_on) }
                     ).FirstOrDefault()
                ;
            if (subTbl2 == null)
            {
                return null;
            }

            // 記事として表示する整形済みの抽象アルバム
            var article =

                (from abs in _context.Abstract_albums
                 where abs.abstract_album_id.Equals(id)
                 select new ArticleAlbum()
                 {
                     Abstract_album_id = abs.abstract_album_id,
                     Title = abs.title,
                     Release_On = subTbl2.release_on
                 }).FirstOrDefault();

            // 抽象アルバムとアルバムを紐づけ
            if (article != null)
            {
                var albums = from lk in _context.Abstract_album_link
                             join alb in _context.Albums on lk.album_id equals alb.album_id
                             join label in _context.Labels.DefaultIfEmpty() on alb.label_id equals label.label_id
                             join media in _context.MediaTypes.DefaultIfEmpty() on alb.media_type equals media.media_type
                             where lk.abstract_album_id.Equals(article.Abstract_album_id)
                             orderby alb.release_on
                             select new DetailAlbum()
                             {
                                 Album_id = alb.album_id,
                                 Title = alb.title,
                                 Code = alb.code,
                                 Release_on = alb.release_on,
                                 Label = label.name ?? "",
                                 Media = media.name ?? ""
                             };
                article.Albums = albums.ToList();

                // アルバムにトラック情報を紐づけ
                foreach (var album in article.Albums)
                {
                    album.Tracks = GetTracks(album.Album_id);
                }
            }

            return article;
        }
    }
}
