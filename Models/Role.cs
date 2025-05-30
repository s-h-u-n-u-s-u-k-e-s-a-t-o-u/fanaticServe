﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("role")]
public partial class Role
{
    /// <summary>
    /// 役割ID
    /// </summary>
    [Key]
    public int Role_Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [StringLength(256)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? Created_At { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? Modified_At { get; set; }
}
