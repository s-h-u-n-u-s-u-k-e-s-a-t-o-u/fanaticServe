using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("abstract_event_link")]
public partial class Abstract_event_link
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    public int id { get; set; }

    /// <summary>
    /// イベントID
    /// </summary>
    public Guid event_id { get; set; }

    /// <summary>
    /// 抽象イベントID
    /// </summary>
    public Guid abstract_event_id { get; set; }
}
