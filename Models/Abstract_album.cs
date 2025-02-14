using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("abstract_album")]
public partial class Abstract_album
{
    /// <summary>
    /// 抽象アルバムID
    /// </summary>
    [Key]
    public Guid Abstract_Album_Id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? Created_At { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Modified_At { get; set; }
}
