using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("media")]
public partial class MediaType
{
    [Key]
    public int Media_Type { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_At { get; set; }
}
