using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("site")]
[Index("site_id", "sequence", Name = "UQ_site_site_id", IsUnique = true)]
public partial class Site
{
    [Key]
    public int id { get; set; }

    /// <summary>
    /// レーベルID
    /// </summary>
    public Guid site_id { get; set; }

    /// <summary>
    /// 表示順
    /// </summary>
    public int sequence { get; set; }

    /// <summary>
    /// 表示名前
    /// </summary>
    [StringLength(256)]
    public string display_name { get; set; } = null!;

    /// <summary>
    /// url
    /// </summary>
    [StringLength(256)]
    public string url { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime created_at { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? modified_at { get; set; }
}
