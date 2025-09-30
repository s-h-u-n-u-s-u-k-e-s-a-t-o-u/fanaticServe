using System.ComponentModel.DataAnnotations;
using fanaticServe.Constants;

namespace fanaticServe.Dto;

public class ReleaseNote
{
    public required string Version { get; set; }

    [DisplayFormat(DataFormatString = Format.DateFormat)]
    public DateTime ReleaseDate { get; set; }

    public required string Note { get; set; }

    public static ReleaseNote[] GetReleaseNotes()
    {
        // Versionは単純に整数でカウントアップする
        var releaseNotes = new ReleaseNote[] {
            new ReleaseNote() {Version = "Pre r2.2" , Note = "SetList、Eventに並べ替え機能追加", ReleaseDate = new DateTime(2025, 9, 30), },
            new ReleaseNote() {Version = "Pre r2.1" , Note = "試用版として公開", ReleaseDate = new DateTime(2025, 7, 1), },
    };

        return releaseNotes;
    }
}

