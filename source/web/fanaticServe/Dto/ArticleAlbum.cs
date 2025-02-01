using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Dto
{
    public class ArticleAlbum
    {
        /// <summary>
        /// 抽象アルバムID
        /// </summary>
        [Key]
        public Guid Abstract_album_id { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(256)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 発売日
        /// </summary>
        [Column(TypeName = "発売日")]
        public DateTime Release_On { get; set; }

        [NotMapped]
        public List<DetailAlbum>? Albums { get; set; }
    }
}
