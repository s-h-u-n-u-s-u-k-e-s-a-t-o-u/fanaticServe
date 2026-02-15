using fanaticServe.Core.Models;

namespace fanaticServe.Core.Dto;

public class ShowablePerson
{
    /// <summary>
    /// 人物ID
    /// </summary>
    public Guid Person_Id { get; set; }

    /// <summary>
    /// 名前
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// カナ
    /// </summary>
    public string Kana { get; set; } = null!;

    /// <summary>
    /// 役割名と局情報のタプルのリスト
    /// </summary>
    public List<RoleWithSong> Songs { get; } = new List<RoleWithSong>();
}
