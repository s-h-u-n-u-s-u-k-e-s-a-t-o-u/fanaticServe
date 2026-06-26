using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Core.Models;

public class Live_Event_Url
{
    [Key]
    public int Live_Rvent_Url_Id { get; set; }
    public Guid Live_Event_Id { get; set; }
    public  string? Url { get; set; }
    public string? Description { get; set; }
}
