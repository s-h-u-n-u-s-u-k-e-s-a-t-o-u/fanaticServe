using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("song")]
public partial class Song
{
    /// <summary>
    /// 楽曲ID
    /// </summary>
    [Key]
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

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_At { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Modified_At { get; set; }
}
