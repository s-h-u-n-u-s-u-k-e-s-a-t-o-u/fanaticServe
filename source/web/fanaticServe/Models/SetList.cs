﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fanaticServe.Models;

[Table("set_list")]
public partial class SetList
{
    /// <summary>
    /// セットリストID
    /// </summary>
    [Key]
    public Guid Set_List_Id { get; set; }

    /// <summary>
    /// イベントID
    /// </summary>
    public Guid Live_Event_Id { get; set; }

    /// <summary>
    /// 曲順
    /// </summary>
    public int Set_List_No { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 楽曲ID
    /// </summary>
    public Guid? Song_id { get; set; }

    /// <summary>
    /// 登録日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Created_at { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime Modified_at { get; set; }
}
