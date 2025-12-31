using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("live_event")]
public partial class LiveEvent
{
    /// <summary>
    /// イベントID
    /// </summary>
    [Key]
    public Guid Live_Event_Id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 会場
    /// </summary>
    [StringLength(256)]
    public string? Place { get; set; }

    /// <summary>
    /// 開演日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? Perform_At { get; set; }

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
