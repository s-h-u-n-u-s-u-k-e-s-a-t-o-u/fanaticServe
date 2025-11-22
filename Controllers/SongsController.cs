using fanaticServe.Data;
using fanaticServe.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Controllers;

public class SongsController : Controller
{
    private readonly FanaticServeContext _context;

    public SongsController(FanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
        ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
        ViewData["CountSort"] = sortOrder == "Count" ? "Count_desc" : "Count";

        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        var songs =
            _context.Songs
            .Select(song => new ShowableSong()
            {
                Song_Id = song.Song_Id,
                Title = song.Title,
                Kana = song.Kana
            });

        if (!String.IsNullOrEmpty(searchString) ){
            songs = songs.Where(s => s.Title.Contains(searchString) || s.Kana.Contains(searchString));
        }

        var songList = await songs.ToListAsync();
        foreach (var song in songList)
        {
            var setlist = await (
                 _context.Set_list
                .Where(sl => sl.Song_Id == song.Song_Id)
                .Join(
                     _context.LiveEvents,
                    sl => sl.Live_Event_Id,
                    le => le.Live_Event_Id,
                    (sl, le) => new { le.Live_Event_Id, le.Perform_At, le.Title })
                .OrderByDescending(sl => sl.Perform_At)
                ).ToListAsync();

            if (setlist != null && setlist.Count > 0)
            {
                song.Count = setlist.Count;
                var rec = setlist
                    .Select(r => new { r.Live_Event_Id, r.Perform_At, r.Title })
                    .First();
                song.LiveEventID = rec.Live_Event_Id;
                song.EventTitle = rec.Title;
                song.LastPeformAt = rec.Perform_At;
            }
        }

        switch (sortOrder)
        {
            case "Count_desc":
                songList = songList.OrderByDescending(s => s.Count).ToList();
                break;
            case "Count":
                songList = songList.OrderBy(s => s.Count).ToList();
                break;
            case "Title_desc":
                songList = songList.OrderByDescending(s => s.Kana).ToList();
                break;
            default:
                songList = songList.OrderBy(s => s.Kana).ToList();
                break;
        }

        return View(songList);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var song =
          await (from s in _context.Songs
                 where s.Song_Id == id
                 select new DetailSong()
                 {
                     Song_Id = s.Song_Id,
                     Title = s.Title,
                     Kana = s.Kana,
                 }).FirstOrDefaultAsync();

        if (song == null)
        {
            return NotFound();
        }

        song.Albums =
            await (
            from album in _context.Albums
            join track in _context.Tracks
            on album.Album_Id equals track.Album_Id
            join media in _context.MediaTypes.DefaultIfEmpty() on album.Media_Type equals media.Media_Type
            where track.Song_Id == id
            orderby album.Release_On
            select new ShowableAlbum()
            {
                Album_id = album.Album_Id,
                Code = album.Code,
                Title = album.Title,
                DetailTitle = album.Title,
                Release_on = album.Release_On,
                Media = media.Name ?? "",
                Track_No = track.Track_No,
                Track_Title = track.Title
            }).OrderBy(a => a.Release_on)
            .ThenBy(suba => suba.Code)
            .ThenBy(suba => suba.Track_No)
            .ToArrayAsync();

        song.LiveEvents =
            await (
            from liveEvent in _context.LiveEvents
            join setList in _context.Set_list.DefaultIfEmpty()
            on liveEvent.Live_Event_Id equals setList.Live_Event_Id
            where setList.Song_Id == id
            orderby liveEvent.Perform_At
            select new ShowableLiveEvent()
            {
                Live_Event_Id = liveEvent.Live_Event_Id,
                Title = liveEvent.Title,
                Place = liveEvent.Place,
                Perform_At = liveEvent.Perform_At
            }
            ).ToArrayAsync();

        return View(song);
    }
}
