using fanaticServe.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ShowableLiveEvent
{
    /// <summary>
    /// イベントID
    /// </summary>
    [DisplayName("イベントID")]
    public Guid Live_Event_Id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 会場
    /// </summary>
    [DisplayName("会場")]
    public string? Place { get; set; }

    /// <summary>
    /// 開演日時
    /// </summary>
    [DisplayName("開演日時")]
    [DisplayFormat(DataFormatString = Format.DateTimeFormat)]
    public DateTime? Perform_At { get; set; }
}
