using fanaticServe.Core.Models;

namespace fanaticServe.Core.Dto
{
    public class RoleWithSong
    {
        required public Role role { get; set; }
        required public Song song { get; set; }
    }
}
