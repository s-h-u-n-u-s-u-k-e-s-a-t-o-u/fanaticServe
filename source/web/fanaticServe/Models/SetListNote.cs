using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("set_list_note")]
public partial class SetListNote
{
    /// <summary>
    /// セットリストID
    /// </summary>
    [Key]
    public Guid set_list_id { get; set; }

    /// <summary>
    /// ノート
    /// </summary>
    public string? note { get; set; }

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
