using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Core.Models;

[Table("abstract_event_link")]
public partial class Abstract_event_link
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// イベントID
    /// </summary>
    public Guid Event_Id { get; set; }

    /// <summary>
    /// 抽象イベントID
    /// </summary>
    public Guid Abstract_Event_Id { get; set; }
}
