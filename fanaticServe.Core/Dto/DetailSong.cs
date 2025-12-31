using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Core.Dto;

public class DetailSong
{
    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid Song_Id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// カナ
    /// </summary>
    [StringLength(256)]
    [DisplayName("カナ")]
    public string Kana { get; set; } = null!;

    public IEnumerable<ShowableAlbum>? Albums { get; set; }
    public IEnumerable<ShowableLiveEvent>? LiveEvents { get; set; }
}
