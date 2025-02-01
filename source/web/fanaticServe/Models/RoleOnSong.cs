using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("roleOnSong")]
public partial class RoleOnSong
{
    [Key]
    public int id { get; set; }

    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid song_id { get; set; }

    /// <summary>
    /// 役割ID
    /// </summary>
    public Guid role_id { get; set; }

    /// <summary>
    /// 人物ID
    /// </summary>
    public Guid person_id { get; set; }

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
