using fanaticServe.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Data;

public partial class FanaticServeContext : DbContext
{
    public FanaticServeContext(DbContextOptions<FanaticServeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abstract_album> Abstract_albums { get; set; }

    public virtual DbSet<Abstract_Album_Note> Abstract_Album_Notes { get; set; }
    
    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Album_Note> Album_Notes { get; set; }   

    public virtual DbSet<Abstract_album_link> Abstract_album_links { get; set; }

    public virtual DbSet<Abstract_event> Abstract_events { get; set; }

    public virtual DbSet<Abstract_Event_Note> Abstract_Event_Notes { get; set; }
    
    public virtual DbSet<Abstract_event_link> Abstract_event_links { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleOnAlbum> RoleOnAlbums { get; set; }

    public virtual DbSet<RoleOnSong> RoleOnSongs { get; set; }

    public virtual DbSet<Set_list> Set_list { get; set; }  

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<LiveEvent> LiveEvents { get; set; }

    public virtual DbSet<Live_Event_Note> Live_Event_Notes { get; set; }
    
    public virtual DbSet<Set_List_Note> SetListNotes { get; set; }    
}
