using fanaticServe.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Dto;

public class ArticleAlbum
{
    private IEnumerable<DetailAlbum>? _albums;

    /// <summary>
    /// 抽象アルバムID
    /// </summary>
    [DisplayName("抽象アルバムID")]
    public Guid Abstract_album_id { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    [DisplayName("タイトル")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 発売日
    /// </summary>
    [DisplayName("発売日")]
    [DisplayFormat(DataFormatString = Format.DateFormat)]
    public DateTime Release_On { get; set; }

    public IEnumerable<DetailAlbum>? Albums
    {
        get => _albums;
        set
        {
            _albums = value;
            if (value != null && value.Count()>0)
            {
                try
                {
                    this.Release_On = value.Min(a => a.Release_on);

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
