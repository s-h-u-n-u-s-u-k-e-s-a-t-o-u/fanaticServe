using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("album")]
public partial class Album
{
    /// <summary>
    /// アルバムID
    /// </summary>
    [Key]
    public Guid Album_Id { get; set; }

    /// <summary>
    /// コード
    /// </summary>
    [StringLength(256)]
    public string? Code { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// メディア種別
    /// </summary>
    public int Media_Type { get; set; }

    /// <summary>
    /// 発売日
    /// </summary>
    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime Release_On { get; set; }

    /// <summary>
    /// レーベルID
    /// </summary>
    public Guid? Label_Id { get; set; }

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
