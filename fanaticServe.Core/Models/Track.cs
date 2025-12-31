using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("track")]
public partial class Track
{
    /// <summary>
    /// トラックID
    /// </summary>
    [Key]
    public Guid Track_Id { get; set; }

    /// <summary>
    /// アルバムID
    /// </summary>
    public Guid Album_Id { get; set; }

    /// <summary>
    /// トラック番号
    /// </summary>
    public int Track_No { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 長さ
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid Song_Id { get; set; }

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
