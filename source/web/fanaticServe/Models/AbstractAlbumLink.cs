using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace fanaticServe.Models;

[Table("abstract_album_link")]
public partial class AbstractAlbumLink
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    public int id { get; set; }

    /// <summary>
    /// アルバムID
    /// </summary>
    public Guid album_id { get; set; }

    /// <summary>
    /// 抽象アルバムID
    /// </summary>
    public Guid abstract_album_id { get; set; }
}
