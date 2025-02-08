using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("abstract_event")]
public partial class AbstractEvent
{
    /// <summary>
    /// 抽象イベントID
    /// </summary>
    [Key]
    public Guid Abstract_event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// ノート
    /// </summary>
    [StringLength(256)]
    public string? Note { get; set; }

}
