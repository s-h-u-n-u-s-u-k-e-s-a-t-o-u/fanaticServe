using fanaticServe.Core.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Core.Dto;

public class ArticleEvent
{
    /// <summary>
    /// 抽象イベントID
    /// </summary>
    public Guid Abstract_event_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// ノート
    /// </summary>
    [StringLength(256)]
    [DisplayName("ノート")]
    public string? Note { get; set; }

    /// <summary>
    /// 公演日
    /// </summary>
    [DisplayName("公演日")]
    [DisplayFormat(DataFormatString = Format.DateFormat)]
    public DateTime? Perform_on { get; set; }

    /// <summary>
    /// イベント詳細
    /// </summary>
    [DisplayName("イベント詳細")]
    public IEnumerable<DetailEvent>? LiveEvents { get; set; } = null;
}
