using fanaticServe.Core.Data;
using fanaticServe.Core.Dto;

namespace fanaticServe.Back;

/// <summary>
/// 星取表を実現するサービスクラス
/// </summary>
public class StarMatrixService: IStarMatrix
{
    /// <summary>
    /// file dataコンテキスト
    /// </summary>
    private readonly IFanaticServeContext _context;

    public StarMatrixService(IFanaticServeContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// headerとrowHeaderを組み合わせて星取表を作成する
    /// </summary>
    public StarMatrix GetStarMatrix()
    {
        // 横にheader数、縦にrowHeader数のboolの二次元配列を作成し、各セルに該当するデータを格納する
        StarMatrixHeader[] header = getHeader();
        StarMatrixRowHeader[] rowHeader = GetRowHeader();

        int columnCount = header.Length;
        int rowCount = rowHeader.Length;

        bool[,] resultRow = new bool[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                // rowHeaderのSetListsにheaderのSongIdが存在するか
                resultRow[i, j] = rowHeader[i].SetLists.Any(s => s.Song_Id == header[j].SongId);
            }
        }

        return new StarMatrix()
        {
            rowCount = rowCount,
            columnCount = columnCount,
            Header = header,
            RowHeader = rowHeader,
            Cells = resultRow
        };
    }

    /// <summary>
    /// ヘッダーに表示する文字列を含むデータを取得する。<br />表示にソート済み。
    /// </summary>
    /// <returns></returns>
    private StarMatrixHeader[] getHeader()   
    {
        StarMatrixHeader[] header = _context.Tracks
            .Join(_context.Albums, t => t.Album_Id, a => a.Album_Id, (t, a) => new { t.Song_Id, a.Release_On, t.Track_No })
            .ToList()  // ここでクライアント側に持ってくる
            .DistinctBy(ta => new { ta.Song_Id, ta.Release_On, ta.Track_No })
            .GroupBy(g => g.Song_Id, ta => new { ta.Release_On, ta.Track_No })
            .Select(g => new
            {
                Song_Id = g.Key,
                FirstRelease = g.OrderBy(x => x.Release_On).ThenBy(i => i.Track_No).First()
            })
            .Join(_context.Songs.ToList(), h => h.Song_Id, s => s.Song_Id, (h, s) => new { h.Song_Id, s.Title, h.FirstRelease.Release_On, h.FirstRelease.Track_No })
            .OrderBy(i => i.Release_On)
            .ThenBy(i => i.Track_No)
            .Select(l => new StarMatrixHeader() { SongId = l.Song_Id, Title = l.Title, ReleaseOn = l.Release_On, Track_No = l.Track_No })
            .ToArray();

        return header;
    }

    private StarMatrixRowHeader[] GetRowHeader()
    {
        // イベントを取得。セットリストとGroupJoin。セットリストが0件のものは除外する。
        var rowHeader = _context.LiveEvents
            .GroupJoin(
                _context.SetLists.Where(s => s.Song_Id != null && s.Singing == true),
                e => e.Live_Event_Id,
                s => s.Live_Event_Id,
                (e, s) => new { Event = e, SetLists = s }
            )
            .Where(es => es.SetLists.Any())
            .Select(es =>
                new StarMatrixRowHeader
                {
                    Live_Event_Id = es.Event.Live_Event_Id,
                    Title = es.Event.Title,
                    Perform_At = es.Event.Perform_At ?? DateTime.Today,
                    SetLists = es.SetLists.OrderBy(sl => sl.Set_List_No).ToList()
                }
            )
            .OrderBy(e => e.Perform_At)
            .ToArray();

        return rowHeader;
    }


}
