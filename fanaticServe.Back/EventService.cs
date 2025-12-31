using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;
using fanaticServe.Core.Models;

namespace fanaticServe.Back;

public class EventService : IEvents
{
    private readonly IFanaticServeContext _context;

    public EventService(IFanaticServeContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortOrder"></param>
    /// <returns></returns>
    public IEnumerable<ShowableEvent> GetAllEvents(string sortOrder)
    {
        var events =
            from absEvent in _context.AbstractEvents
            join link in _context.AbstractEventLinks
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

        return events;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortOrder"></param>
    /// <param name="searchString"></param>
    /// <returns></returns>
    public IEnumerable<ArticleEvent> GetAllEventArticles(string sortOrder, string searchString)
    {
        var events = _context.LiveEvents;

        // 検索文字列によるフィルタリング
        if (!String.IsNullOrEmpty(searchString))
        {
            events = events.Where(e => e.Title.Contains(searchString));
        }

        // イベントリンク + イベント
        var joinedEvents =
            _context.AbstractEventLinks
            .Join(events,
                link => link.Event_Id,
                liveEvent => liveEvent.Live_Event_Id,
                (link, liveEvent) => new { Abstract_Event_Id = link.Abstract_Event_Id, Live_Event = liveEvent })
            .GroupBy(x => x.Abstract_Event_Id)
            ;

        // 抽象イベントごとにまとめる
        var filteredArticles = _context.AbstractEvents
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
                    liveEvent.SetLists = GetSetList(liveEvent.Live_event_id);
                    liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;
                }

                article.Perform_on = article.LiveEvents.Min(le => le.Perform_at);
            }

        }

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
        return filteredArticles;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public DetailEvent GetDetailEvent(Guid eventId)
    {
        var liveEvent = _context.LiveEvents
            .Where(le => le.Live_Event_Id == eventId)
            .Select(le => new DetailEvent
            {
                Live_event_id = le.Live_Event_Id,
                Title = le.Title,
                Place = le.Place,
                Perform_at = le.Perform_At
            })
            .First();

        liveEvent.SetLists = GetSetList(eventId);
        liveEvent.Note = GetLiveEventNote(liveEvent.Live_event_id)?.Note;

        return liveEvent;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="abstId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ArticleEvent GetEventGroup(Guid abstId, string sortOrder)
    {
        var eventGroup = (
    from absEvent in _context.AbstractEvents
    where absEvent.Abstract_Event_Id == abstId
    select new ArticleEvent()
    {
        Abstract_event_id = absEvent.Abstract_Event_Id,
        Title = absEvent.Title
    }
    ).First();

        eventGroup.LiveEvents = (
            from linkedList in _context.AbstractEventLinks
            join liveEvent in _context.LiveEvents
            on linkedList.Event_Id equals liveEvent.Live_Event_Id
            where linkedList.Abstract_Event_Id == abstId
            select new DetailEvent()
            {
                Live_event_id = liveEvent.Live_Event_Id,
                Title = liveEvent.Title,
                Perform_at = liveEvent.Perform_At,
                Place = liveEvent.Place
            }
            ).ToList();

        if (eventGroup.LiveEvents != null && eventGroup.LiveEvents.Any())
        {
            eventGroup.Perform_on = eventGroup.LiveEvents.Min(e => e.Perform_at);

            foreach (var liveEvent in eventGroup.LiveEvents)
            {
                liveEvent.SetLists = GetSetList(liveEvent.Live_event_id);
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

        return eventGroup;
    }

    private List<ShowableSetList>? GetSetList(Guid Live_event_id)
    {
        return (
                   from setList in _context.SetLists
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
                       Note = (subT == null) ? "" : subT.Note ?? ""
                   }
               ).ToList();
    }

    private Live_Event_Note? GetLiveEventNote(Guid liveEventId)
    {
        return _context.LiveEventNotes.SingleOrDefault(le => le.Live_Event_Id == liveEventId);
    }
}
