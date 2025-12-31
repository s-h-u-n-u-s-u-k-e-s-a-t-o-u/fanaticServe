using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("person")]
public partial class Person
{
    /// <summary>
    /// 人物ID
    /// </summary>
    [Key]
    public Guid Person_Id { get; set; }

    /// <summary>
    /// 名前
    /// </summary>
    [StringLength(256)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// カナ
    /// </summary>
    [StringLength(256)]
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
