using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("abstract_event_note")]
public partial class Abstract_Event_Note
{
    /// <summary>
    /// 抽象イベントID
    /// </summary>
    [Key]
    public Guid Abstract_Event_Id { get; set; }

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
