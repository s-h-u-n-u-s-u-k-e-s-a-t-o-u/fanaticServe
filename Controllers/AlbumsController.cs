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
        private readonly FanaticServeContext _context;

        public AlbumsController(FanaticServeContext context)
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
                join lk in _context.Abstract_album_link on absal.abstract_album_id equals lk.Abstract_Album_Id
                join alb in _context.Albums on lk.Album_Id equals alb.Album_Id
                join media in _context.MediaTypes.DefaultIfEmpty() on alb.Media_Type equals media.Media_Type
                orderby alb.Release_On
                select new ShowableAlbum()
                {
                    Abstract_album_id = absal.abstract_album_id,
                    Title = absal.title,
                    Album_id = alb.Album_Id,
                    DetailTitle = alb.Title,
                    Release_on = alb.Release_On,
                    Media = media.Name ?? ""
                };

            return View(await records.ToListAsync());
        }

        [HttpGet]
        public IActionResult Articles()
        {
            var subTbl2 =
                from link in _context.Abstract_album_link
                join album in _context.Albums on link.Album_Id equals album.Album_Id
                group new { link, album } by link.Abstract_Album_Id into tbl2
                select new { abstract_album_id = tbl2.Key, release_on = tbl2.Min(m => m.album.Release_On) }
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
                             join alb in _context.Albums on lk.Album_Id equals alb.Album_Id
                             join label in _context.Labels.DefaultIfEmpty() on alb.Label_Id equals label.Label_Id
                             join media in _context.MediaTypes.DefaultIfEmpty() on alb.Media_Type equals media.Media_Type
                             where article.Abstract_album_id.Equals(lk.Abstract_Album_Id)
                             orderby alb.Release_On
                             select new DetailAlbum()
                             {
                                 Album_id = alb.Album_Id,
                                 Title = alb.Title,
                                 Code = alb.Code,
                                 Release_on = alb.Release_On,
                                 Label = label.Name ?? "",
                                 Media = media.Name ?? ""
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
                         join alb in _context.Albums on lk.Album_Id equals alb.Album_Id
                         join label in _context.Labels.DefaultIfEmpty() on alb.Label_Id equals label.Label_Id
                         join media in _context.MediaTypes.DefaultIfEmpty() on alb.Media_Type equals media.Media_Type
                         where album_id.Equals(lk.Album_Id)
                         orderby alb.Release_On
                         select new DetailAlbum()
                         {
                             Album_id = alb.Album_Id,
                             Title = alb.Title,
                             Code = alb.Code,
                             Release_on = alb.Release_On,
                             Label = label.Name ?? "",
                             Media = media.Name ?? ""
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
                where album_id.Equals(trk.Album_Id)
                orderby trk.Track_No
                select new Track()
                {
                    Track_Id = trk.Track_Id,
                    Track_No = trk.Track_No,
                    Title = trk.Title,
                    Length = trk.Length,
                    Song_Id = trk.Song_Id
                };
            return tracks.ToList();
        }

        private ArticleAlbum? GetArticleAlbum(Guid id)
        {
            var subTbl2 =

                    (from link in _context.Abstract_album_link
                     join album in _context.Albums on link.Album_Id equals album.Album_Id
                     group new { link, album } by link.Abstract_Album_Id into tbl2
                     select new { abstract_album_id = tbl2.Key, release_on = tbl2.Min(m => m.album.Release_On) }
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
                             join alb in _context.Albums on lk.Album_Id equals alb.Album_Id
                             join label in _context.Labels.DefaultIfEmpty() on alb.Label_Id equals label.Label_Id
                             join media in _context.MediaTypes.DefaultIfEmpty() on alb.Media_Type equals media.Media_Type
                             where lk.Abstract_Album_Id.Equals(article.Abstract_album_id)
                             orderby alb.Release_On
                             select new DetailAlbum()
                             {
                                 Album_id = alb.Album_Id,
                                 Title = alb.Title,
                                 Code = alb.Code,
                                 Release_on = alb.Release_On,
                                 Label = label.Name ?? "",
                                 Media = media.Name ?? ""
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
