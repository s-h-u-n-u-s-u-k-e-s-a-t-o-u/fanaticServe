using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("site")]
public partial class Site
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// レーベルID
    /// </summary>
    public Guid Site_Id { get; set; }

    /// <summary>
    /// 表示順
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    /// 表示名前
    /// </summary>
    [StringLength(256)]
    public string Display_Name { get; set; } = null!;

    /// <summary>
    /// url
    /// </summary>
    [StringLength(256)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_At { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? Modified_At { get; set; }
}
