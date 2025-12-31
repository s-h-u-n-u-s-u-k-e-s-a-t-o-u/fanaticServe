using fanaticServe.Core.Dto;

namespace fanaticServe.Core.Data;

public interface ISongs
{
    public IEnumerable<ShowableSong> GetAllSongs(string sortOrder, string searchString);
    public DetailSong GetSong(Guid songId);
    }
