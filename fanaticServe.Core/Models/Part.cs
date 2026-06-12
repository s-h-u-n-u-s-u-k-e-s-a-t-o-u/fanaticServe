using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

/// <summary>
/// パート区分マスタ
/// </summary>
[Table("Part")]
public class Part
{
    /// <summary>
    ///  パート区分
    /// </summary>
    public int Part_Type { get; set; }

    /// <summary>
    /// パート値
    /// </summary>
    public  string? Part_Value { get; set; }

    /// <summary>
    /// 登録日付
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_At { get; set; }

    /// <summary>
    /// 更新日付
    /// </summary>
    [Column(TypeName = "datetime")]
    public decimal Modified_At { get; set; }
}
