using fanaticServe.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ShowableEvent
{
    /// <summary>
    /// 抽象イベントID
    /// </summary>
    [Required]
    public Guid Abstract_event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [DisplayName("ツアー/イベント")]
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 抽象イベントID
    /// </summary>
    public Guid Event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [DisplayName("タイトル")]
    public string DetailTitle { get; set; } = null!;

    /// <summary>
    /// ノート
    /// </summary>
    [DisplayName("ノート")]
    public string? Note { get; set; }

    /// <summary>
    /// 開演時間
    /// </summary>
    [DisplayName("開演時間")]
    [DisplayFormat(DataFormatString = Format.DateTimeFormat)]
    public DateTime? Perform_at { get; set; }

}
