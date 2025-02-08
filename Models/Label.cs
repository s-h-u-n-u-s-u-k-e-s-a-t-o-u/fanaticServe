using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("label")]
public partial class Label
{
    /// <summary>
    /// レーベルID
    /// </summary>
    [Key]
    public Guid Label_Id { get; set; }

    /// <summary>
    /// 組織ID
    /// </summary>
    public Guid Organization_Id { get; set; }

    /// <summary>
    /// 名前
    /// </summary>
    [StringLength(256)]
    public string Name { get; set; } = null!;

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
