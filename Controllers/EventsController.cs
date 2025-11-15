using fanaticServe.Data;
using fanaticServe.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace fanaticServe.Controllers;

public class EventsController : Controller
{
    private readonly IFanaticServeContext _context;

    public EventsController(IFanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder)
    {
        // ソート条件の初期化
        ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["TitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";

        var events =
            from absEvent in _context.Abstract_events
            join link in _context.Abstract_event_links
            on absEvent.Abstract_Event_Id equals link.Abstract_Event_Id
            join liveEvent in _context.LiveEvents
            on link.Event_Id equals liveEvent.Live_Event_Id
            select new ShowableEvent()
            {
                Abstract_event_id = absEvent.Abstract_Event_Id,
                Title = absEvent.Title,
                Event_id = liveEvent.Live_Event_Id,
                DetailTitle = liveEvent.Title,
                Perform_at = liveEvent.Perform_At
            };

        // ソート条件に応じて並び替え
        switch (sortOrder)
        {
            case "date_desc":
                events = events.OrderByDescending(e => e.Perform_at);
                break;
            case "title":
                events = events.OrderBy(e => e.DetailTitle);
                break;
            case "title_desc":
                events = events.OrderByDescending(e => e.DetailTitle);
                break;
            default:
                events = events.OrderBy(e => e.Perform_at);
                break;
        }

        return View(await events.ToListAsync());
    }

    // GET: Events/Details/5
    [HttpGet]
    public async Task<IActionResult> Detail(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var liveEvent = await _context.LiveEvents
            .Where(le => le.Live_Event_Id == id)
            .Select(le => new DetailEvent
            {
                Live_event_id = le.Live_Event_Id,
                Title = le.Title,
                Place = le.Place,
                Perform_at = le.Perform_At
            })
            .FirstOrDefaultAsync();

        if (liveEvent == null)
        {
            return NotFound();
        }

        liveEvent.SetLists = GetSelList(id.Value);
        liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;

        return View(liveEvent);
    }

    [HttpGet]
    public async Task<IActionResult> EventGroup(Guid? id, string sortOrder)
    {
        if (id == null)
        {
            return NotFound();
        }

        // ソート条件の初期化
        ViewData["GroupDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["GroupTitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";

        var eventGroup = await (
            from absEvent in _context.Abstract_events
            where absEvent.Abstract_Event_Id == id
            select new ArticleEvent()
            {
                Abstract_event_id = absEvent.Abstract_Event_Id,
                Title = absEvent.Title
            }
            ).FirstOrDefaultAsync();

        if (eventGroup == null)
        {
            return NotFound();
        }

        eventGroup.LiveEvents = await (
            from linkedList in _context.Abstract_event_links
            join liveEvent in _context.LiveEvents
            on linkedList.Event_Id equals liveEvent.Live_Event_Id
            where linkedList.Abstract_Event_Id == id
            select new DetailEvent()
            {
                Live_event_id = liveEvent.Live_Event_Id,
                Title = liveEvent.Title,
                Perform_at = liveEvent.Perform_At,
                Place = liveEvent.Place
            }
            ).ToListAsync();

        if (eventGroup.LiveEvents != null && eventGroup.LiveEvents.Any())
        {
            eventGroup.Perform_on = eventGroup.LiveEvents.Min(e => e.Perform_at);

            foreach (var liveEvent in eventGroup.LiveEvents)
            {
                liveEvent.SetLists = GetSelList(liveEvent.Live_event_id);
                liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;
            }
        }

        if (eventGroup.LiveEvents != null)
        {
            switch (sortOrder)
            {
                case "title":
                    eventGroup.LiveEvents = eventGroup.LiveEvents.OrderBy(e => e.Title);
                    break;
                case "date_desc":
                    eventGroup.LiveEvents = eventGroup.LiveEvents.OrderByDescending(e => e.Perform_at);
                    break;
                case "title_desc":
                    eventGroup.LiveEvents = eventGroup.LiveEvents.OrderByDescending(e => e.Title);

                    break;
                default:
                    eventGroup.LiveEvents = eventGroup.LiveEvents.OrderBy(e => e.Perform_at);
                    break;
            }
        }
        return View(eventGroup);
    }

    [HttpGet]
    public IActionResult Articles(string sortOrder, string searchString)
    {
        // ソート条件の初期化
        ViewData["ArticleDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["ArticleTitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        var events = _context.LiveEvents;

        // 検索文字列によるフィルタリング
        if (!String.IsNullOrEmpty(searchString))
        {
            events = events.Where(e => e.Title.Contains(searchString));
        }

        // イベントリンク + イベント
        var joinedEvents =
            _context.Abstract_event_links
            .Join(events,
                link => link.Event_Id,
                liveEvent => liveEvent.Live_Event_Id,
                (link, liveEvent) => new { Abstract_Event_Id = link.Abstract_Event_Id, Live_Event = liveEvent })
            .GroupBy(x => x.Abstract_Event_Id)
            ;

        // 抽象イベントごとにまとめる
        var  filteredArticles = _context.Abstract_events
            .Join(joinedEvents,
            ae => ae.Abstract_Event_Id,
            je => je.Key,
            (ae, je) => new { ae, je })
            .Select(x => new ArticleEvent()
            {
                Abstract_event_id = x.ae.Abstract_Event_Id,
                Title = x.ae.Title,
                LiveEvents = x.je.Select(je => new DetailEvent()
                {
                    Live_event_id = je.Live_Event.Live_Event_Id,
                    Title = je.Live_Event.Title,
                    Place = je.Live_Event.Place,
                    Perform_at = je.Live_Event.Perform_At,
                }).ToList()
            }
            ).ToList();

        foreach (var article in filteredArticles)
        {
            if (article.LiveEvents != null)
            {
                foreach (var liveEvent in article.LiveEvents)
                {
                    liveEvent.SetLists = GetSelList(liveEvent.Live_event_id);
                    liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;
                }

                article.Perform_on = article.LiveEvents.Min(le => le.Perform_at);
            }

        }

        //var articles =
        //    (from absEvent in _context.Abstract_events
        //     select new ArticleEvent()
        //     {
        //         Abstract_event_id = absEvent.Abstract_Event_Id,
        //         Title = absEvent.Title
        //     }).ToList();
        //// イベント詳細を取得
        //foreach (var article in articles)
        //{
        //    var query =
        //        from linkedList in _context.Abstract_event_links
        //        join liveEvent in _context.LiveEvents
        //        on linkedList.Event_Id equals liveEvent.Live_Event_Id
        //        where linkedList.Abstract_Event_Id == article.Abstract_event_id
        //        select new DetailEvent()
        //        {
        //            Live_event_id = liveEvent.Live_Event_Id,
        //            Title = liveEvent.Title,
        //            Place = liveEvent.Place,
        //            Perform_at = liveEvent.Perform_At
        //        };

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        // 部分一致（SQL に翻訳されます）
        //        query = query.Where(d => d.Title.Contains(searchString));
        //    }

        //    article.LiveEvents = query.OrderBy(e => e.Perform_at).ToList();

        //    if (article.LiveEvents != null && article.LiveEvents.Any())
        //    {
        //        article.Perform_on = article.LiveEvents.Min(e => e.Perform_at);
        //        foreach (var liveEvent in article.LiveEvents)
        //        {
        //            liveEvent.SetLists = GetSelList(liveEvent.Live_event_id);
        //            liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;
        //        }
        //    }
        //}
        //var filteredArticles = articles.Where(a => a.LiveEvents != null && a.LiveEvents.Any());

        // 全体の並び替え（記事レベル）
        switch (sortOrder)
        {
            case "date_desc":
                filteredArticles = filteredArticles.OrderByDescending(e => e.Perform_on).ToList();
                break;
            case "title":
                filteredArticles = filteredArticles.OrderBy(e => e.Title).ToList();

                break;
            case "title_desc":
                filteredArticles = filteredArticles.OrderByDescending(e => e.Title).ToList();
                break;
            default:
                filteredArticles = filteredArticles.OrderBy(e => e.Perform_on).ToList();
                break;
        }

        return View(filteredArticles);
    }

    private List<ShowableSetList>? GetSelList(Guid Live_event_id)
    {
        var query = _context.Set_lists
            .Where(setList => setList.Live_Event_Id == Live_event_id)
            .OrderBy(setList => setList.Set_List_No)
            ;

        return query.GroupJoin(_context.Set_List_Notes,
            setlist => setlist.Set_List_Id,
            note => note.Set_List_Id,
            (setlist, note) => new { s = setlist, n = note }
            )
            .SelectMany(o => o.n.DefaultIfEmpty(),
            (obj, p2) => new ShowableSetList()
            {
                Set_List_Id = obj.s.Set_List_Id,
                Live_Event_Id = obj.s.Live_Event_Id,
                Set_List_No = obj.s.Set_List_No,
                Title = obj.s.Title,
                Song_id = obj.s.Song_Id,
                Note = p2 == null ? "" : p2.Note,
            }).ToList();
    }

    private Live_Event_Note? GetLiveEventNote(Guid liveEventId)
    {
        return _context.Live_Event_Notes.SingleOrDefault(le => le.Live_Event_Id == liveEventId);
    }

}
