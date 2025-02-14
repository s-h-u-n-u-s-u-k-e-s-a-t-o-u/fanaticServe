using fanaticServe.Constants;
using fanaticServe.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class DetailAlbum
{
    public Guid Album_id { get; set; }

    [DisplayName("タイトル")]
    public required string Title { get; set; }

    [DisplayName("コード")]
    public string? Code { get; set; }

    [DisplayName("発売日")]
    [DisplayFormat(DataFormatString = Format.DateFormat)]
    public DateTime Release_on { get; set; }

    [DisplayName("レーベル")]
    public string? Label { get; set; }

    [DisplayName("メディア")]
    public string? Media { get; set; }

    public IEnumerable<Track>? Tracks { get; set; }
}
