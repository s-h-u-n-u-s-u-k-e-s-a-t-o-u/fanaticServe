using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("set_list")]
public partial class Set_list
{
    /// <summary>
    /// セットリストID
    /// </summary>
    [Key]
    public Guid set_list_id { get; set; }

    /// <summary>
    /// イベントID
    /// </summary>
    public Guid live_event_id { get; set; }

    /// <summary>
    /// 曲順
    /// </summary>
    public int set_list_no { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string title { get; set; } = null!;

    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid? song_id { get; set; }

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
