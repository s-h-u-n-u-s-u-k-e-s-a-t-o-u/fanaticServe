using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("media")]
public partial class MediaType
{
    [Key]
    public int media_type { get; set; }

    [StringLength(256)]
    public string name { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime created_at { get; set; }
}
