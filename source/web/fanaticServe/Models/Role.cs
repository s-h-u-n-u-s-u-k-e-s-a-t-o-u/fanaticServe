using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("role")]
public partial class Role
{
    /// <summary>
    /// 役割ID
    /// </summary>
    [Key]
    public int role_id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [StringLength(256)]
    public string name { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? created_at { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? modified_at { get; set; }
}
