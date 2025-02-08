using fanaticServe.Models;
using System.ComponentModel;

namespace fanaticServe.Dto;

public class DetailAlbum
{
    public Guid Album_id { get; set; }

    [DisplayName("タイトル")]
    public string Title { get; set; }

    [DisplayName("コード")]
    public string? Code { get; set; }

    [DisplayName("発売日")]
    public DateTime Release_on { get; set; }

    [DisplayName("レーベル")]
    public string? Label { get; set; }

    [DisplayName("メディア")]
    public string? Media { get; set; }

    public IEnumerable<Track>? Tracks { get; set; }
}
