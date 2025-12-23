using System.Linq;
using fanaticServe.Models;

namespace fanaticServe.Data;

public interface IFanaticServeContext
{
    IQueryable<Abstract_album> Abstract_albums { get; }
    IQueryable<Abstract_Album_Note> Abstract_Album_Notes { get; }
    IQueryable<Abstract_album_link> Abstract_album_links { get; }
    IQueryable<Album> Albums { get; }
    IQueryable<Album_Note> Album_Notes { get; }
    IQueryable<MediaType> MediaTypes { get; }
    IQueryable<Label> Labels { get; }
    IQueryable<Track> Tracks { get; }

    IQueryable<Abstract_event> Abstract_events { get; }
    IQueryable<Abstract_event_link> Abstract_event_links { get; }
    IQueryable<LiveEvent> LiveEvents { get; }
    IQueryable<Live_Event_Note> Live_Event_Notes { get; }

    IQueryable<Set_list> Set_lists { get; }
    IQueryable<Set_List_Note> Set_List_Notes { get; }
    IQueryable<Song> Songs { get; }
}