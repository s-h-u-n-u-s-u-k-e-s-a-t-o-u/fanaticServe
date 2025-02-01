using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("live_event")]
public partial class LiveEvent
{
    /// <summary>
    /// イベントID
    /// </summary>
    [Key]
    public Guid live_event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string title { get; set; } = null!;

    /// <summary>
    /// 会場
    /// </summary>
    [StringLength(256)]
    public string? place { get; set; }

    /// <summary>
    /// 開演日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? perform_at { get; set; }

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime created_at { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime modified_at { get; set; }
}
