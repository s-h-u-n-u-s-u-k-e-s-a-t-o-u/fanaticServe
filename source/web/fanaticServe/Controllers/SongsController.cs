using fanaticServe.Data;
using fanaticServe.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Controllers;

public class SongsController : Controller
{
    private readonly fanaticServeContext _context;

    public SongsController(fanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var songs = _context.Songs;
        return View(await songs.OrderBy(s => s.kana).ToArrayAsync());
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
                 where s.song_id == id
                 select new DetailSong()
                 {
                     song_id = s.song_id,
                     title = s.title,
                     kana = s.kana,
                 }).FirstOrDefaultAsync();

        if (song == null)
        {
            return NotFound();
        }

        song.Albums =
            await (
            from album in _context.Albums
            join tack in _context.Tracks
            on album.album_id equals tack.album_id
            join media in _context.MediaTypes.DefaultIfEmpty() on album.media_type equals media.media_type
            where tack.song_id == id
            orderby album.release_on
            select new ShowableAlbum()
            {
                Album_id = album.album_id,
                Title = album.title,
                DetailTitle = album.title,
                Release_on = album.release_on,
                Media = media.name ?? ""

            }).ToArrayAsync();

        song.LiveEvents =
            await (
            from liveEvent in _context.LiveEvents
            join setList in _context.SetLists
            on liveEvent.live_event_id equals setList.Live_Event_Id
            where setList.Song_id == id
            select new ShowableLiveEvent()
            {
                Live_Event_Id = liveEvent.live_event_id,
                Title = liveEvent.title,
                Place = liveEvent.place,
                Perform_At = liveEvent.perform_at
            }
            ).ToArrayAsync();

        return View(song);
    }
}
