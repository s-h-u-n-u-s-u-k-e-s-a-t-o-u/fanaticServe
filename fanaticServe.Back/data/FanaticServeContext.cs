using fanaticServe.Core.Data;
using fanaticServe.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Back.Data;

public partial class FanaticServeContext : DbContext, IFanaticServeContext
{
    public FanaticServeContext(DbContextOptions<FanaticServeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abstract_album> v_Abstract_albums { get; set; }
    public virtual DbSet<Abstract_Album_Note> v_Abstract_Album_Notes { get; set; }
    public virtual DbSet<Abstract_album_link> v_Abstract_album_links { get; set; }
    public virtual DbSet<Album> v_Albums { get; set; }
    public virtual DbSet<Album_Note> v_Album_Notes { get; set; }

    public virtual DbSet<MediaType> v_MediaTypes { get; set; }
    public virtual DbSet<Label> v_Labels { get; set; }
    public virtual DbSet<Track> v_Tracks { get; set; }

    public virtual DbSet<Abstract_event> v_Abstract_events { get; set; }
    public virtual DbSet<Abstract_event_link> v_Abstract_event_links { get; set; }
    public virtual DbSet<Abstract_Event_Note> v_Abstract_Event_Notes { get; set; }

    public virtual DbSet<LiveEvent> v_LiveEvents { get; set; }
    public virtual DbSet<Live_Event_Note> v_Live_Event_Notes { get; set; }
    public virtual DbSet<Set_list> v_Set_list { get; set; }
    public virtual DbSet<Set_List_Note> v_SetListNotes { get; set; }

    public virtual DbSet<Song> v_Songs { get; set; }
    public virtual DbSet<RoleOnSong> v_RoleOnSongs { get; set; }
    public virtual DbSet<Role> v_Roles { get; set; }
    public virtual DbSet<Organization> v_Organizations { get; set; }

    public virtual DbSet<Person> v_People { get; set; }
    public virtual DbSet<RoleOnAlbum> v_RoleOnAlbums { get; set; }
    public virtual DbSet<Site> v_Sites { get; set; }

    // IFanaticServeContextの実装
    public IQueryable<Abstract_album> AbstractAlbums { get { return v_Abstract_albums; } }
    public IQueryable<Abstract_Album_Note> AbstractAlbumNotes { get { return v_Abstract_Album_Notes; } }
    public IQueryable<Abstract_album_link> AbstractAlbumLinks { get { return v_Abstract_album_links; } }
    public IQueryable<Album> Albums { get { return v_Albums; } }
    public IQueryable<Album_Note> AlbumNotes { get { return v_Album_Notes; } }

    public IQueryable<MediaType> MediaTypes { get { return v_MediaTypes; } }
    public IQueryable<Label> Labels { get { return v_Labels; } }
    public IQueryable<Track> Tracks { get { return v_Tracks; } }

    public IQueryable<Abstract_event> AbstractEvents { get { return v_Abstract_events; } }
    public IQueryable<Abstract_event_link> AbstractEventLinks { get { return v_Abstract_event_links; } }

    public IQueryable<Abstract_Event_Note> AbstractEventNote { get { return v_Abstract_Event_Notes; } }
    
    public IQueryable<LiveEvent> LiveEvents { get { return v_LiveEvents; } }
    public IQueryable<Live_Event_Note> LiveEventNotes { get { return v_Live_Event_Notes; } }
    public IQueryable<Set_list> SetLists { get { return v_Set_list; } }
    public IQueryable<Set_List_Note> SetListNotes { get { return v_SetListNotes; } }

    public IQueryable<Song> Songs { get { return v_Songs; } }
    public IQueryable<RoleOnSong> RoleOnSongs { get { return v_RoleOnSongs; } }
    public IQueryable<Role> Roles { get { return v_Roles; } }
    public IQueryable<Person> People { get { return v_People; } }
}
