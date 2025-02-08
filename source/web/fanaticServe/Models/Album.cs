using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("album")]
public partial class Album
{
    /// <summary>
    /// アルバムID
    /// </summary>
    [Key]
    public Guid album_id { get; set; }

    /// <summary>
    /// コード
    /// </summary>
    [StringLength(256)]
    public string? code { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string title { get; set; } = null!;

    /// <summary>
    /// メディア種別
    /// </summary>
    public int media_type { get; set; }

    /// <summary>
    /// リリース日
    /// </summary>
    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime release_on { get; set; }

    /// <summary>
    /// レーベルID
    /// </summary>
    public Guid? label_id { get; set; }

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
