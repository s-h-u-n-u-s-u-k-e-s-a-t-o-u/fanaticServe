using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace fanaticServe.Dto
{
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? Perform_at { get; set; }

    }
}
