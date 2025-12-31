using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("abstract_album_link")]
public partial class Abstract_album_link
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// アルバムID
    /// </summary>
    public Guid Album_Id { get; set; }

    /// <summary>
    /// 抽象アルバムID
    /// </summary>
    public Guid Abstract_Album_Id { get; set; }
}
