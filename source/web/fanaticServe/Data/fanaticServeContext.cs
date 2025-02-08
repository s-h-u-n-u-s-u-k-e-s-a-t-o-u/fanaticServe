using fanaticServe.Models;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Data;

public partial class fanaticServeContext : DbContext
{
    public fanaticServeContext(DbContextOptions<fanaticServeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbstractAlbum> Abstract_albums { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AbstractAlbumLink> Abstract_album_link { get; set; }

    public virtual DbSet<AbstractEvent> AbstractEvent { get; set; }

    public virtual DbSet<AbstractEventLink> Abstract_Event_Link { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleOnAlbum> RoleOnAlbums { get; set; }

    public virtual DbSet<RoleOnSong> RoleOnSongs { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<LiveEvent> LiveEvents { get; set; }

    public virtual DbSet<SetList> SetLists { get; set; }

    public virtual DbSet<SetListNote> SetListNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbstractAlbum>(entity =>
        {
            entity.ToTable("abstract_album", tb => tb.HasComment(""));

            entity.Property(e => e.abstract_album_id)
                .ValueGeneratedNever()
                .HasComment("抽象アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.Property(e => e.album_id)
                .ValueGeneratedNever()
                .HasComment("アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.label_id).HasComment("レーベルID");
            entity.Property(e => e.media_type).HasComment("メディア種別");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.Property(e => e.label_id)
                .ValueGeneratedNever()
                .HasComment("レーベルID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.name).HasComment("名前");
            entity.Property(e => e.organization_id).HasComment("組織ID");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.Property(e => e.media_type).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasComment("登録日時");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.Property(e => e.organization_id)
                .ValueGeneratedNever()
                .HasComment("組織ID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.kana).HasComment("カナ");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.name).HasComment("名前");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.person_id)
                .ValueGeneratedNever()
                .HasComment("人物ID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.kana).HasComment("カナ");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.name).HasComment("名前");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.role_id)
                .ValueGeneratedNever()
                .HasComment("役割ID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.name).HasComment("名称");
        });

        modelBuilder.Entity<RoleOnAlbum>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.album_id).HasComment("アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.person_id).HasComment("人物ID");
            entity.Property(e => e.role_id).HasComment("役割ID");
        });

        modelBuilder.Entity<RoleOnSong>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.person_id).HasComment("人物ID");
            entity.Property(e => e.role_id).HasComment("役割ID");
            entity.Property(e => e.song_id).HasComment("楽曲ID");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.Property(e => e.site_id).HasComment("レーベルID");
            entity.Property(e => e.sequence).HasComment("表示順");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.display_name).HasComment("表示名前");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.url).HasComment("url");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.Property(e => e.song_id)
                .ValueGeneratedNever()
                .HasComment("楽曲ID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.kana).HasComment("カナ");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.Property(e => e.track_id)
                .ValueGeneratedNever()
                .HasComment("トラックID");
            entity.Property(e => e.album_id).HasComment("アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.length).HasComment("長さ");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.song_id).HasComment("楽曲ID");
            entity.Property(e => e.title).HasComment("タイトル");
            entity.Property(e => e.track_no).HasComment("トラック番号");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
