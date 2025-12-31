using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("abstract_album_note")]
public partial class Abstract_Album_Note
{
    /// <summary>
    /// アルバムID
    /// </summary>
    [Key]
    public Guid Album_id { get; set; }

    /// <summary>
    /// ノート
    /// </summary>
    public string? Note { get; set; }

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
