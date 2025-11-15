using fanaticServe.Data;
using fanaticServe.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
        // 並び変えの判定
        ViewData["DateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["TitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        // 一覧表形式でアルバム情報を表示する
        var records =
            from absal in _context.Abstract_albums
            join lk in _context.Abstract_album_links on absal.Abstract_Album_Id equals lk.Abstract_Album_Id
            join alb in _context.Albums on lk.Album_Id equals alb.Album_Id
            join media in _context.MediaTypes.DefaultIfEmpty() on alb.Media_Type equals media.Media_Type
            select new ShowableAlbum()
            {
                Abstract_album_id = absal.Abstract_Album_Id,
                Title = absal.Title,
                Album_id = alb.Album_Id,
                DetailTitle = alb.Title,
                Release_on = alb.Release_On,
                Media = media.Name ?? ""
            };

        if (!string.IsNullOrEmpty(searchString))
        {
            // 部分一致（SQL に翻訳されます）
            records = records.Where(d => d.DetailTitle.Contains(searchString));
        }

        switch (sortOrder)
        {
            case "title":
                records = records.OrderBy(r => r.Title);
                break;
            case "titleDesending":
                records = records.OrderByDescending(r => r.Title);
                break;
            case "dateDesending":
                records = records.OrderByDescending(r => r.Release_on);
                break;
            default:
                records = records.OrderBy(r => r.Release_on);
                break;
        }

        return View(await records.ToListAsync());
    }

    // GET: Albums/Details/5
    [HttpGet]
    public async Task<IActionResult> Detail(Guid? id)
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

        return View(await album);
    }

    [HttpGet]
    public async Task<IActionResult> Articles(string sortOrder, string searchString)
    {
        // 並び変えの判定
        ViewData["ArticleDateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["ArticleTitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        var articles =
            await (
            from absAlbum in _context.Abstract_albums
            select new ArticleAlbum()
            {
                Abstract_album_id = absAlbum.Abstract_Album_Id,
                Title = absAlbum.Title,
            }
            ).ToListAsync();

        foreach (var article in articles)
        {
            article.Albums = await (
                from absAlbumLink in _context.Abstract_album_links

                join album in _context.Albums
                on absAlbumLink.Album_Id equals album.Album_Id

                join media in _context.MediaTypes
                on album.Media_Type equals media.Media_Type into mt
                from mediaType in mt.DefaultIfEmpty()

                join l in _context.Labels
                on album.Label_Id equals l.Label_Id into lb
                from labelName in lb.DefaultIfEmpty()

                where absAlbumLink.Abstract_Album_Id.Equals(article.Abstract_album_id)
                orderby album.Release_On
                select new DetailAlbum()
                {
                    Album_id = album.Album_Id,
                    Title = album.Title,
                    Code = album.Code,
                    Release_on = album.Release_On,
                    Label = labelName.Name,
                    Media = mediaType.Name
                }
                ).ToListAsync();

            if (article.Albums != null)
            {
                foreach (var album in article.Albums)
                {
                    album.Tracks = await GetTracks(album.Album_id, searchString);
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    article.Albums = article.Albums.Where(a => a.Tracks != null && a.Tracks.Any());
                }
            }
        }

        if (!string.IsNullOrEmpty(searchString))
        {
            articles = articles.Where(ar => ar.Albums != null && ar.Albums.Any()).ToList();
        }

        switch (sortOrder)
        {
            case "title":
                articles = articles.OrderBy(r => r.Title).ToList();
                break;
            case "titleDesending":
                articles = articles.OrderByDescending(r => r.Title).ToList();
                break;
            case "dateDesending":
                articles = articles.OrderByDescending(r => r.Release_On).ToList();
                break;
            default:
                articles = articles.OrderBy(r => r.Release_On).ToList();
                break;
        }
        return View(articles);
    }

    [HttpGet]
    public async Task<IActionResult> AlbumGroup(Guid? id, string sortOrder)
    {
        if (id == null)
        {
            return NotFound();
        }

        // 並び変えの判定
        ViewData["GroupDateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateDesending" : "";
        ViewData["GroupTitleSort"] = sortOrder == "title" ? "titleDesending" : "title";

        var article = await GetArticleAlbum(id.Value);

        if (article != null)
        {
            switch (sortOrder)
            {
                case "titleDesending":
                    article.Albums = article.Albums?.OrderByDescending(r => r.Title);
                    break;

                case "title":
                    article.Albums = article.Albums?.OrderBy(r => r.Title);
                    break;

                case "dateDesending":
                    article.Albums = article.Albums?.OrderByDescending(r => r.Release_on);
                    break;

                default:
                    article.Albums = article.Albums?.OrderBy(r => r.Release_on);
                    break;
            }
        }
        return View(article);
    }

    private async Task<DetailAlbum?> GetDetailAlbum(Guid album_id)
    {
        var album =
            await (
            from lk in _context.Abstract_album_links
            join alb in _context.Albums
            on lk.Album_Id equals alb.Album_Id
            where album_id.Equals(lk.Album_Id)
            orderby alb.Release_On
            join label in _context.Labels
            on alb.Label_Id equals label.Label_Id into joinedTable1
            from jt1 in joinedTable1.DefaultIfEmpty()
            join media in _context.MediaTypes
            on alb.Media_Type equals media.Media_Type into joinedTable2
            from jt2 in joinedTable2.DefaultIfEmpty()
            select new DetailAlbum()
            {
                Album_id = alb.Album_Id,
                Title = alb.Title,
                Code = alb.Code,
                Release_on = alb.Release_On,
                Label = jt1.Name ?? "",
                Media = jt2.Name ?? ""
            }
            ).FirstOrDefaultAsync();

        if (album != null)
        {
            // アルバムにトラック情報を紐づけ                
            album.Tracks = await GetTracks(album.Album_id);
        }

        return album;
    }

    private async Task<List<Track>> GetTracks(Guid album_id, String searchString = "")
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

        if (!String.IsNullOrEmpty(searchString))
        {
            tracks = tracks.Where(t => t.Title.Contains(searchString));
        }

        return await tracks.ToListAsync();
    }

    private async Task<ArticleAlbum?> GetArticleAlbum(Guid id)
    {
        var subTbl2 =
        await (
        from link in _context.Abstract_album_links
        join album in _context.Albums on link.Album_Id equals album.Album_Id
        group new { link, album } by link.Abstract_Album_Id into tbl2
        select new { abstract_album_id = tbl2.Key, release_on = tbl2.Min(m => m.album.Release_On) }
        ).FirstOrDefaultAsync();

        if (subTbl2 == null)
        {
            return null;
        }

        // 記事として表示する整形済みの抽象アルバム
        var article =
            await (
            from abs in _context.Abstract_albums
            where abs.Abstract_Album_Id.Equals(id)
            select new ArticleAlbum()
            {
                Abstract_album_id = abs.Abstract_Album_Id,
                Title = abs.Title,
                Release_On = subTbl2.release_on
            }
            ).FirstOrDefaultAsync();

        // 抽象アルバムとアルバムを紐づけ
        if (article != null)
        {
            article.Albums =
                await (
                from lk in _context.Abstract_album_links
                join alb in _context.Albums
                on lk.Album_Id equals alb.Album_Id
                where lk.Abstract_Album_Id.Equals(article.Abstract_album_id)
                join label in _context.Labels on alb.Label_Id equals label.Label_Id into joinedTable1
                from jt1 in joinedTable1.DefaultIfEmpty()
                join media in _context.MediaTypes on alb.Media_Type equals media.Media_Type into joinedTable2
                from jt2 in joinedTable2.DefaultIfEmpty()
                select new DetailAlbum()
                {
                    Album_id = alb.Album_Id,
                    Title = alb.Title,
                    Code = alb.Code,
                    Release_on = alb.Release_On,
                    Label = jt1.Name ?? "",
                    Media = jt2.Name ?? ""
                }).ToListAsync();

            // アルバムにトラック情報を紐づけ
            foreach (var album in article.Albums)
            {
                album.Tracks = await GetTracks(album.Album_id);
            }
        }

        return article;
    }
}
