using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("track")]
public partial class Track
{
    /// <summary>
    /// トラックID
    /// </summary>
    [Key]
    public Guid track_id { get; set; }

    /// <summary>
    /// アルバムID
    /// </summary>
    public Guid album_id { get; set; }

    /// <summary>
    /// トラック番号
    /// </summary>
    public int track_no { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string title { get; set; } = null!;

    /// <summary>
    /// 長さ
    /// </summary>
    public int length { get; set; }

    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid song_id { get; set; }

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
