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
            new ReleaseNote() {Version = "Pre.6" , Note = "レイアウト調整。2013年～2022年のセットリストデータを追加", ReleaseDate = new DateTime(2025, 5, 17), },
            new ReleaseNote() {Version = "Pre.5" , Note = "曲一覧に最新の歌唱イベントを表示。2019年～2023年のセットリストデータを追加", ReleaseDate = new DateTime(2025, 3, 23), },
            new ReleaseNote() {Version = "Pre.4" , Note = "曲ごとに披露したイベントを表示。2024年のセットリストデータを追加", ReleaseDate = new DateTime(2025, 3, 16), },
            new ReleaseNote() {Version = "Pre.3" , Note = "card styleを全体に適用", ReleaseDate = new DateTime(2025, 2, 19), },
            new ReleaseNote() {Version = "Pre.2" , Note = "楽曲詳細の表示を変更", ReleaseDate = new DateTime(2025, 2, 11), },
            new ReleaseNote() {Version = "Pre.1" ,Note = "プレリリース", ReleaseDate = new DateTime(2025,2, 9), },
    };

        return releaseNotes;
    }
}

