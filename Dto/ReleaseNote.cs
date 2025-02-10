using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ReleaseNote
{
    public required string Version { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime ReleaseDate { get; set; }

    public required string Note { get; set; }

    public static ReleaseNote[] GetReleaseNotes()
    {
        // Versionは単純に整数でカウントアップする
        var releaseNotes = new ReleaseNote[] {
            new ReleaseNote() {Version = "Pre.2" , Note = "楽曲詳細の表示を変更", ReleaseDate = new DateTime(2021, 2, 11), },
            new ReleaseNote() { Version = "Pre.1" ,Note = "プレリリース", ReleaseDate = new DateTime(2025,2, 9), },
    };

        return releaseNotes;
    }
}

