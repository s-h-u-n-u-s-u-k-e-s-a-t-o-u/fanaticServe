using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace fanaticServe.Dto
{
    public class DetailSong
    {
        /// <summary>
        /// 楽曲ID
        /// </summary>
        public Guid song_id { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(256)]
        [DisplayName("タイトル")]
        public string title { get; set; } = null!;

        /// <summary>
        /// カナ
        /// </summary>
        [StringLength(256)]
        [DisplayName("カナ")]
        public string kana { get; set; } = null!;

        public IEnumerable<ShowableAlbum>? Albums { get; set; }
        public IEnumerable<ShowableLiveEvent>? LiveEvents { get; set; }

    }
}
