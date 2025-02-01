using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

public partial class Fanatic_serveContext : DbContext
{
    public Fanatic_serveContext(DbContextOptions<Fanatic_serveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abstract_album> abstract_albums { get; set; }

    public virtual DbSet<Abstract_album_link> abstract_album_links { get; set; }

    public virtual DbSet<Abstract_event> abstract_events { get; set; }

    public virtual DbSet<Abstract_event_link> abstract_event_links { get; set; }

    public virtual DbSet<Album> albums { get; set; }

    public virtual DbSet<Label> labels { get; set; }

    public virtual DbSet<LiveEvent> live_events { get; set; }

    public virtual DbSet<MediaType> media { get; set; }

    public virtual DbSet<Organization> organizations { get; set; }

    public virtual DbSet<Person> people { get; set; }

    public virtual DbSet<Role> roles { get; set; }

    public virtual DbSet<RoleOnAlbum> roleOnAlbums { get; set; }

    public virtual DbSet<RoleOnSong> roleOnSongs { get; set; }

    public virtual DbSet<Set_list> set_lists { get; set; }

    public virtual DbSet<Site> sites { get; set; }

    public virtual DbSet<Song> songs { get; set; }

    public virtual DbSet<Track> tracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abstract_album>(entity =>
        {
            entity.Property(e => e.abstract_album_id)
                .ValueGeneratedNever()
                .HasComment("抽象アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Abstract_album_link>(entity =>
        {
            entity.Property(e => e.id).HasComment("ID");
            entity.Property(e => e.abstract_album_id).HasComment("抽象アルバムID");
            entity.Property(e => e.album_id).HasComment("アルバムID");
        });

        modelBuilder.Entity<Abstract_event>(entity =>
        {
            entity.Property(e => e.abstract_event_id)
                .ValueGeneratedNever()
                .HasComment("抽象いベントID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.note).HasComment("ノート");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Abstract_event_link>(entity =>
        {
            entity.Property(e => e.id).HasComment("ID");
            entity.Property(e => e.abstract_event_id).HasComment("抽象イベントID");
            entity.Property(e => e.event_id).HasComment("イベントID");
        });

        modelBuilder.Entity<Abstract_album>(entity =>
        {
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
            entity.Property(e => e.code).HasComment("コード");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.label_id).HasComment("レーベルID");
            entity.Property(e => e.media_type).HasComment("メディア種別");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.release_on).HasComment("リリース日");
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

        modelBuilder.Entity<LiveEvent>(entity =>
        {
            entity.Property(e => e.live_event_id)
                .ValueGeneratedNever()
                .HasComment("イベントID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.perform_at).HasComment("開演日時");
            entity.Property(e => e.place).HasComment("会場");
            entity.Property(e => e.title).HasComment("タイトル");
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
            entity.Property(e => e.role_id).HasComment("役割ID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.name).HasComment("名称");
        });

        modelBuilder.Entity<RoleOnAlbum>(entity =>
        {
            entity.Property(e => e.album_id).HasComment("アルバムID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.person_id).HasComment("人物ID");
            entity.Property(e => e.role_id).HasComment("役割ID");
        });

        modelBuilder.Entity<RoleOnSong>(entity =>
        {
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.person_id).HasComment("人物ID");
            entity.Property(e => e.role_id).HasComment("役割ID");
            entity.Property(e => e.song_id).HasComment("楽曲ID");
        });

        modelBuilder.Entity<Set_list>(entity =>
        {
            entity.Property(e => e.set_list_id)
                .ValueGeneratedNever()
                .HasComment("セットリストID");
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.live_event_id).HasComment("イベントID");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.set_list_no).HasComment("曲順");
            entity.Property(e => e.song_id).HasComment("楽曲ID");
            entity.Property(e => e.title).HasComment("タイトル");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.Property(e => e.created_at).HasComment("登録日時");
            entity.Property(e => e.display_name).HasComment("表示名前");
            entity.Property(e => e.modified_at).HasComment("更新日時");
            entity.Property(e => e.sequence).HasComment("表示順");
            entity.Property(e => e.site_id).HasComment("レーベルID");
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
