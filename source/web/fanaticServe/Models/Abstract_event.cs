using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("abstract_event")]
public partial class Abstract_event
{
    /// <summary>
    /// 抽象いベントID
    /// </summary>
    [Key]
    public Guid abstract_event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string title { get; set; } = null!;

    /// <summary>
    /// ノート
    /// </summary>
    [StringLength(256)]
    public string? note { get; set; }

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? created_at { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime modified_at { get; set; }
}
