using System.ComponentModel;

namespace fanaticServe.Core.Dto;

public class ShowableSetList
{
    /// <summary>
    /// セットリストID
    /// </summary>
    [DisplayName("セットリストID")]
    public Guid Set_List_Id { get; set; }

    /// <summary>
    /// イベントID
    /// </summary>
    [DisplayName("イベントID")]
    public Guid Live_Event_Id { get; set; }

    /// <summary>
    /// 曲順
    /// </summary>
    [DisplayName("曲順")]
    public int Set_List_No { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 楽曲ID
    /// </summary>
    [DisplayName("楽曲ID")]
    public Guid? Song_id { get; set; }

    /// <summary>
    ///  歌唱フラグ
    /// </summary>
    public bool Singing { get; set; }

    /// <summary>
    ///  メドレーフラグ
    /// </summary>
    public bool Medley { get; set; }

    /// <summary>
    ///  カバーフラグ
    /// </summary>
    public bool Cover { get; set; }

    /// <summary>
    ///  パート区分
    /// </summary>
    public int Part_Type { get; set; }

    /// <summary>
    /// ノート
    /// </summary>
    [DisplayName("ノート")]
    public string? Note { get; set; }

}
