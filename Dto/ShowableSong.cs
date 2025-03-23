﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ShowableSong
{

    /// <summary>
    /// 楽曲ID
    /// </summary>
    [Key]
    public Guid Song_Id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [StringLength(256)]
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// カナ
    /// </summary>
    [StringLength(256)]
    [DisplayName("カナ")]
    public string Kana { get; set; } = null!;

    [DisplayName("歌唱回数")]
    public int? Count { get; set; } = 0;

    public string? EventTitle { get; set; }

    public DateTime? LastPeformAt { get; set; }

}
