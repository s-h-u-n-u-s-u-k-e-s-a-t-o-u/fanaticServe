using fanaticServe.Data;
using fanaticServe.Dto;
using fanaticServe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Controllers;

public class EventsController : Controller
{
    private readonly fanaticServeContext _context;

    public EventsController(fanaticServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var events =
            from absEvent in _context.AbstractEvent
            join link in _context.Abstract_Event_Link
            on absEvent.Abstract_event_id equals link.abstract_event_id
            join liveEvent in _context.LiveEvents
            on link.event_id equals liveEvent.live_event_id
            orderby liveEvent.perform_at
            select new ShowableEvent()
            {
                Abstract_event_id = absEvent.Abstract_event_id,
                Title = absEvent.Title,
                Event_id = liveEvent.live_event_id,
                DetailTitle = liveEvent.title,
                Note = absEvent.Note,
                Perform_at = liveEvent.perform_at
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
            where le.live_event_id == id
            select new DetailEvent
            {
                Live_event_id = id.Value,
                Title = le.title,
                Place = le.place,
                Perform_at = le.perform_at
            }
            ).FirstOrDefaultAsync();

        if (liveEvent == null)
        {
            return NotFound();
        }

        liveEvent.SetLists =
            await (
            from setList in _context.SetLists
            join slNote in _context.SetListNotes.DefaultIfEmpty()
            on setList.Set_List_Id equals slNote.set_list_id
            where setList.Live_Event_Id == id
            orderby setList.Set_List_No
            select new ShowableSetList
            {
                Set_List_Id = setList.Set_List_Id,
                Live_Event_Id = id.Value,
                Set_List_No = setList.Set_List_No,
                Title = setList.Title,
                Song_id = setList.Song_id,
                Note = slNote.note ?? ""
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
            from absEvent in _context.AbstractEvent
            where absEvent.Abstract_event_id == id
            select new ArticleEvent()
            {
                Abstract_event_id = absEvent.Abstract_event_id,
                Title = absEvent.Title,
                Note = absEvent.Note
            }
            ).FirstOrDefaultAsync();
        if (eventGroup == null)
        {
            return NotFound();
        }

        eventGroup.LiveEvents = await (
            from linkedList in _context.Abstract_Event_Link
            join liveEvent in _context.LiveEvents
            on linkedList.event_id equals liveEvent.live_event_id
            where linkedList.abstract_event_id == id
            select new DetailEvent()
            {
                Live_event_id = liveEvent.live_event_id,
                Title = liveEvent.title,
                Perform_at = liveEvent.perform_at,
                Place = liveEvent.place
            }
            ).ToListAsync();

        if (eventGroup.LiveEvents != null)
        {
            eventGroup.Perform_on = eventGroup.LiveEvents.Min(e => e.Perform_at);

            foreach (var liveEvent in eventGroup.LiveEvents)
            {
                liveEvent.SetLists = await (
                    from setList in _context.SetLists
                    join slNote in _context.SetListNotes.DefaultIfEmpty()
                    on setList.Set_List_Id equals slNote.set_list_id
                    where setList.Live_Event_Id == liveEvent.Live_event_id
                    orderby setList.Set_List_No
                    select new ShowableSetList()
                    {
                        Set_List_Id = setList.Set_List_Id,
                        Live_Event_Id = setList.Live_Event_Id,
                        Set_List_No = setList.Set_List_No,
                        Title = setList.Title,
                        Song_id = setList.Song_id,
                        Note = slNote.note ?? ""
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
            from absEvent in _context.AbstractEvent
            select new ArticleEvent()
            {
                Abstract_event_id = absEvent.Abstract_event_id,
                Title = absEvent.Title,
                Note = absEvent.Note
            }
            ).ToListAsync();

        // イベント詳細を取得
        foreach (var article in articles)
        {
            article.LiveEvents = await (
                from linkedList in _context.Abstract_Event_Link
                join liveEvent in _context.LiveEvents
                on linkedList.event_id equals liveEvent.live_event_id
                where linkedList.abstract_event_id == article.Abstract_event_id
                orderby liveEvent.perform_at
                select new DetailEvent()
                {
                    Live_event_id = liveEvent.live_event_id,
                    Title = liveEvent.title,
                    Place = liveEvent.place,
                    Perform_at = liveEvent.perform_at
                }
                ).ToListAsync();

            if (article.LiveEvents != null)
            {
                article.Perform_on = article.LiveEvents.Min(e => e.Perform_at);
                foreach (var liveEvent in article.LiveEvents)
                {
                    liveEvent.SetLists = await (
                        from setList in _context.SetLists
                        join slNote in _context.SetListNotes.DefaultIfEmpty()
                        on setList.Set_List_Id equals slNote.set_list_id
                        where setList.Live_Event_Id == liveEvent.Live_event_id
                        orderby setList.Set_List_No
                        select new ShowableSetList()
                        {
                            Set_List_Id = setList.Set_List_Id,
                            Live_Event_Id = setList.Live_Event_Id,
                            Set_List_No = setList.Set_List_No,
                            Title = setList.Title,
                            Song_id = setList.Song_id,
                            Note = slNote.note ?? ""
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
