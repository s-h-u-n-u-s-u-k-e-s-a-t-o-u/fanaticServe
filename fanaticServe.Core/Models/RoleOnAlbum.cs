using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("roleOnAlbum")]
public partial class RoleOnAlbum
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// アルバムID
    /// </summary>
    public Guid Album_Id { get; set; }

    /// <summary>
    /// 役割ID
    /// </summary>
    public Guid Role_Id { get; set; }

    /// <summary>
    /// 人物ID
    /// </summary>
    public Guid Person_Id { get; set; }

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_At { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Modified_At { get; set; }
}
