using fanaticServe.Core.Models;

namespace fanaticServe.Core.Data;

public interface IFanaticServeContext
{
    IQueryable<Abstract_album> AbstractAlbums { get; }
    IQueryable<Abstract_Album_Note> AbstractAlbumNotes { get; }
    IQueryable<Abstract_album_link> AbstractAlbumLinks { get; }
    IQueryable<Album> Albums { get; }
    IQueryable<Album_Note> AlbumNotes { get; }
    IQueryable<MediaType> MediaTypes { get; }
    IQueryable<Label> Labels { get; }
    IQueryable<Track> Tracks { get; }

    IQueryable<Abstract_event> AbstractEvents { get; }
    IQueryable<Abstract_event_link> AbstractEventLinks { get; }
    IQueryable<LiveEvent> LiveEvents { get; }
    IQueryable<Live_Event_Note> LiveEventNotes { get; }

    IQueryable<Set_list> SetLists { get; }
    IQueryable<Set_List_Note> SetListNotes { get; }
    IQueryable<Song> Songs { get; }
}