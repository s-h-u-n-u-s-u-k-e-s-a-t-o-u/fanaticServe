<<<<<<< HEAD
﻿using fanaticServe.Back;
using fanaticServe.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace fanaticServe.Controllers;

public class SongsController : Controller
{
    private readonly IFanaticServeContext _context;

    public SongsController(IFanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder, string searchString)
    {
        ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
        ViewData["CountSort"] = sortOrder == "Count" ? "Count_desc" : "Count";

        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        return View(new SongService(_context).GetAllSongs(sortOrder, searchString));
    }

    [HttpGet]
    public IActionResult Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return View(new SongService(_context).GetSong(id.Value));
=======
﻿using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;

namespace fanaticServe.Back;

public class SongService : ISongs
{
    private readonly IFanaticServeContext _context;

    public SongService(IFanaticServeContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// 全曲のリストを取得する
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ShowableSong> GetAllSongs(string sortOrder, string searchString)
    {
        var songs =
            _context.Songs
            .Select(song => new ShowableSong()
            {
                Song_Id = song.Song_Id,
                Title = song.Title,
                Kana = song.Kana
            });

        if (!String.IsNullOrEmpty(searchString))
        {
            songs = songs.Where(s => s.Title.Contains(searchString) || s.Kana.Contains(searchString));
        }

        var songList = songs.ToList();
        foreach (var song in songList)
        {
            var setlist = (
                 _context.SetLists
                .Where(sl => sl.Song_Id == song.Song_Id)
                .Join(
                     _context.LiveEvents,
                    sl => sl.Live_Event_Id,
                    le => le.Live_Event_Id,
                    (sl, le) => new { le.Live_Event_Id, le.Perform_At, le.Title })
                .OrderByDescending(sl => sl.Perform_At)
                ).ToList();

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

        return sortOrder switch
        {
            "Count_desc" => songList.OrderByDescending(s => s.Count).ToList(),
            "Count" => songList.OrderBy(s => s.Count).ToList(),
            "Title_desc" => songList.OrderByDescending(s => s.Kana).ToList(),
            _ => songList.OrderBy(s => s.Kana).ToList()
        };
    }

    /// <summary>
    /// 1曲の情報を取得する
    /// </summary>
    /// <param name="songId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public DetailSong GetSong(Guid songId)
    {
        var song =
           (from s in _context.Songs
            where s.Song_Id == songId
            select new DetailSong()
            {
                Song_Id = s.Song_Id,
                Title = s.Title,
                Kana = s.Kana,
            }).First();

        song.Albums =
            (
            from album in _context.Albums
            join track in _context.Tracks
            on album.Album_Id equals track.Album_Id
            join media in _context.MediaTypes.DefaultIfEmpty() on album.Media_Type equals media.Media_Type
            where track.Song_Id == songId
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
            .ToArray();

        song.LiveEvents =
            (
            from liveEvent in _context.LiveEvents
            join setList in _context.SetLists.DefaultIfEmpty()
            on liveEvent.Live_Event_Id equals setList.Live_Event_Id
            where setList.Song_Id == songId
            orderby liveEvent.Perform_At
            select new ShowableLiveEvent()
            {
                Live_Event_Id = liveEvent.Live_Event_Id,
                Title = liveEvent.Title,
                Place = liveEvent.Place,
                Perform_At = liveEvent.Perform_At
            }
            ).ToArray();

        return song;
>>>>>>> ディレクトリ構成変更
    }
}
