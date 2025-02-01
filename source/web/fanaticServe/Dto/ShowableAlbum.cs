using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto
{
    public class ShowableAlbum
    {
        public Guid abstract_album_id { get; set; }

        [DisplayName("アルバム")]
        public string title { get; set; }

        public Guid album_id { get; set; }

        [DisplayName("バリエーション")]
        public string detailTitle { get; set; }

        [DisplayName("発売日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime release_on { get; set; }

        [DisplayName("媒体")]
        public string media { get; set; }

    }
}
