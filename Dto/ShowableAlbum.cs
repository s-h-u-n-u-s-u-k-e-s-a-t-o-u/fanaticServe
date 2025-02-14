using fanaticServe.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ShowableAlbum
{
    public Guid Abstract_album_id { get; set; }

    [DisplayName("アルバム")]
    public required string Title { get; set; }

    public Guid Album_id { get; set; }

    public string? Code { get; set; }

    [DisplayName("バリエーション")]
    public required string DetailTitle { get; set; }

    [DisplayName("発売日")]
    [DisplayFormat(DataFormatString = Format.DateFormat)]
    public DateTime Release_on { get; set; }

    [DisplayName("媒体")]
    public string? Media { get; set; }

    [DisplayName("トラック No")]
    public int Track_No { get; set; }

    [DisplayName("曲名")]
    public string Track_Title { get; set; } = null!;
}
