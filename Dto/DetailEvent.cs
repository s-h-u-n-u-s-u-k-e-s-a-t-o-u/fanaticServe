using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;
public class DetailEvent
    {
       public Guid Live_event_id { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(256)]
        [DisplayName("タイトル")]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 会場
        /// </summary>
        [StringLength(256)]
        [DisplayName("会場")]
        public string? Place { get; set; }

        /// <summary>
        /// 開演日時
        /// </summary>
        [DisplayName("開演日時")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? Perform_at { get; set; }

        /// <summary>
        /// セットリスト
        /// </summary>
        [DisplayName("セットリスト")]
        public IEnumerable<ShowableSetList>? SetLists { get; set; }
    }

