using fanaticServe.Data;
using fanaticServe.Dto;
using fanaticServe.Models;
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
    public async Task<IActionResult> Index()
    {
        var events =
            from absEvent in _context.Abstract_events
            join link in _context.Abstract_event_links
            on absEvent.Abstract_Event_Id equals link.Abstract_Event_Id
            join liveEvent in _context.LiveEvents
            on link.Event_Id equals liveEvent.Live_Event_Id
            orderby liveEvent.Perform_At
            select new ShowableEvent()
            {
                Abstract_event_id = absEvent.Abstract_Event_Id,
                Title = absEvent.Title,
                Event_id = liveEvent.Live_Event_Id,
                DetailTitle = liveEvent.Title,
                Perform_at = liveEvent.Perform_At
            };

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

        var liveEvent = await (
            from le in _context.LiveEvents
            where le.Live_Event_Id == id
            select new DetailEvent
            {
                Live_event_id = id.Value,
                Title = le.Title,
                Place = le.Place,
                Perform_at = le.Perform_At
            }
            ).FirstOrDefaultAsync();

        if (liveEvent == null)
        {
            return NotFound();
        }

        liveEvent.SetLists =
            await (
            from setList in _context.Set_list
            join slNote in _context.SetListNotes.DefaultIfEmpty()
            on setList.Set_List_Id equals slNote.Set_List_Id
            where setList.Live_Event_Id == id
            orderby setList.Set_List_No
            select new ShowableSetList
            {
                Set_List_Id = setList.Set_List_Id,
                Live_Event_Id = id.Value,
                Set_List_No = setList.Set_List_No,
                Title = setList.Title,
                Song_id = setList.Song_Id,
                Note = slNote.Note ?? ""
            }
            ).ToListAsync();

        return View(liveEvent);
    }

    [HttpGet]
    public async Task<IActionResult> EventGroup(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

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

        if (eventGroup.LiveEvents != null)
        {
            eventGroup.Perform_on = eventGroup.LiveEvents.Min(e => e.Perform_at);

            foreach (var liveEvent in eventGroup.LiveEvents)
            {
                liveEvent.SetLists = await (
                    from setList in _context.Set_list
                    join slNote in _context.SetListNotes.DefaultIfEmpty()
                    on setList.Set_List_Id equals slNote.Set_List_Id
                    where setList.Live_Event_Id == liveEvent.Live_event_id
                    orderby setList.Set_List_No
                    select new ShowableSetList()
                    {
                        Set_List_Id = setList.Set_List_Id,
                        Live_Event_Id = setList.Live_Event_Id,
                        Set_List_No = setList.Set_List_No,
                        Title = setList.Title,
                        Song_id = setList.Song_Id,
                        Note = slNote.Note ?? ""
                    }
                    ).ToListAsync();
            }
        }
        return View(eventGroup);
    }

    [HttpGet]
    public async Task<IActionResult> Articles()
    {
        var articles =
            await (
            from absEvent in _context.Abstract_events
            select new ArticleEvent()
            {
                Abstract_event_id = absEvent.Abstract_Event_Id,
                Title = absEvent.Title
            }
            ).ToListAsync();

        // イベント詳細を取得
        foreach (var article in articles)
        {
            article.LiveEvents = await (
                from linkedList in _context.Abstract_event_links
                join liveEvent in _context.LiveEvents
                on linkedList.Event_Id equals liveEvent.Live_Event_Id
                where linkedList.Abstract_Event_Id == article.Abstract_event_id
                orderby liveEvent.Perform_At
                select new DetailEvent()
                {
                    Live_event_id = liveEvent.Live_Event_Id,
                    Title = liveEvent.Title,
                    Place = liveEvent.Place,
                    Perform_at = liveEvent.Perform_At
                }
                ).ToListAsync();

            if (article.LiveEvents != null)
            {
                article.Perform_on = article.LiveEvents.Min(e => e.Perform_at);
                foreach (var liveEvent in article.LiveEvents)
                {
                    liveEvent.SetLists = await (
                        from setList in _context.Set_list
                        join slNote in _context.SetListNotes.DefaultIfEmpty()
                        on setList.Set_List_Id equals slNote.Set_List_Id
                        where setList.Live_Event_Id == liveEvent.Live_event_id
                        orderby setList.Set_List_No
                        select new ShowableSetList()
                        {
                            Set_List_Id = setList.Set_List_Id,
                            Live_Event_Id = setList.Live_Event_Id,
                            Set_List_No = setList.Set_List_No,
                            Title = setList.Title,
                            Song_id = setList.Song_Id,
                            Note = slNote.Note ?? ""
                        }
                        ).ToListAsync();
                }
            }
        }

        return View(
            (from article in articles orderby article.Perform_on select article).ToArray()
            );
    }
}
