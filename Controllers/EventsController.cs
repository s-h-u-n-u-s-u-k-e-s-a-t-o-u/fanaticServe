using fanaticServe.Core.Dto;
using fanaticServe.Core.Models;
using fanaticServe.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Controllers;

public class EventsController : Controller
{
    private readonly FanaticServeContext _context;

    public EventsController(FanaticServeContext context)
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

        liveEvent.SetLists = await GetSelList(id.Value);
        liveEvent.Note = (await GetLiveEventNote(liveEvent.Live_event_id))?.Note;

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
                liveEvent.SetLists = await GetSelList(liveEvent.Live_event_id);
                liveEvent.Note = (await GetLiveEventNote(liveEvent.Live_event_id))?.Note;
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
    public async Task<IActionResult> Articles(string sortOrder, string searchString)
    {
        // ソート条件の初期化
        ViewData["ArticleDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
        ViewData["ArticleTitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";
        ViewData["CurrentSort"] = sortOrder;
        ViewData["CurrentFilter"] = searchString;

        var articles =
                        await (
            from absEvent in _context.Abstract_events
            select new ArticleEvent()
            {
                Abstract_event_id = absEvent.Abstract_Event_Id,
                Title = absEvent.Title
            }).ToListAsync();

        // イベント詳細を取得
        foreach (var article in articles)
        {
            var query =
                from linkedList in _context.Abstract_event_links
                join liveEvent in _context.LiveEvents
                on linkedList.Event_Id equals liveEvent.Live_Event_Id
                where linkedList.Abstract_Event_Id == article.Abstract_event_id
                select new DetailEvent()
                {
                    Live_event_id = liveEvent.Live_Event_Id,
                    Title = liveEvent.Title,
                    Place = liveEvent.Place,
                    Perform_at = liveEvent.Perform_At
                };

            if (!string.IsNullOrEmpty(searchString))
            {
                // 部分一致（SQL に翻訳されます）
                query = query.Where(d => d.Title.Contains(searchString));
            }
            query = query.OrderBy(e => e.Perform_at);

            article.LiveEvents = await query.ToListAsync();

            if (article.LiveEvents != null && article.LiveEvents.Any())
            {
                article.Perform_on = article.LiveEvents.Min(e => e.Perform_at);
                foreach (var liveEvent in article.LiveEvents)
                {
                    liveEvent.SetLists = await GetSelList(liveEvent.Live_event_id);
                    liveEvent.Note = (await GetLiveEventNote(liveEvent.Live_event_id))?.Note;
                }
            }
        }

        var filteredArticles = articles.Where(a => a.LiveEvents != null && a.LiveEvents.Any());

        // 全体の並び替え（記事レベル）
        switch (sortOrder)
        {
            case "date_desc":
                //                articles = articles.OrderByDescending(e => e.Perform_on).ToList();
                filteredArticles = filteredArticles.OrderByDescending(e => e.Perform_on);
                break;
            case "title":
                //                articles = articles.OrderBy(e => e.Title).ToList();
                filteredArticles = filteredArticles.OrderBy(e => e.Title);

                break;
            case "title_desc":
                //                articles = articles.OrderByDescending(e => e.Title).ToList();
                filteredArticles = filteredArticles.OrderByDescending(e => e.Title);
                break;
            default:
                //                articles = articles.OrderBy(e => e.Perform_on).ToList();
                filteredArticles = filteredArticles.OrderBy(e => e.Perform_on);
                break;
        }

        return View(filteredArticles);
    }

    private async Task<List<ShowableSetList>> GetSelList(Guid Live_event_id)
    {
        return await (
            from setList in _context.Set_list
            join slNote in _context.SetListNotes
            on setList.Set_List_Id equals slNote.Set_List_Id into joinT
            where setList.Live_Event_Id == Live_event_id
            orderby setList.Set_List_No
            from subT in joinT.DefaultIfEmpty()
            select new ShowableSetList()
            {
                Set_List_Id = setList.Set_List_Id,
                Live_Event_Id = setList.Live_Event_Id,
                Set_List_No = setList.Set_List_No,
                Title = setList.Title,
                Song_id = setList.Song_Id,
                Note = subT.Note ?? ""
            }
        ).ToListAsync();
    }

    async private Task<Live_Event_Note?> GetLiveEventNote(Guid liveEventId)
    {
        return await _context.Live_Event_Notes.SingleOrDefaultAsync(le => le.Live_Event_Id == liveEventId);
    }

}
