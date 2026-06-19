using fanaticServe.Core.Models;

namespace fanaticServe.Core.Dto;

public class StarMatrix
{
    public int rowCount { get; set; }
    public int columnCount { get; set; }
    public required StarMatrixHeader[] Header { get; set; }
    public required StarMatrixRowHeader[] RowHeader { get; set; }
    public required bool[,] Cells { get; set; } 
}

public class StarMatrixHeader
{
    public required Guid SongId { get; set; }
    public required string Title { get; set; }
    public DateTime ReleaseOn { get; set; }
    public int Track_No { get; set; }
}

public class StarMatrixRowHeader
{
    public required Guid Live_Event_Id { get; set; }
    public required string Title { get; set; }
    public DateTime Perform_At { get; set; }
    public required List<Set_list> SetLists { get; set; }
}

