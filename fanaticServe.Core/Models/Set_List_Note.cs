using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("set_list_note")]
public partial class Set_List_Note
{
    /// <summary>
    /// セットリストID
    /// </summary>
    [Key]
    public Guid Set_List_Id { get; set; }

    /// <summary>
    /// ノート
    /// </summary>
    public string? Note { get; set; }

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
