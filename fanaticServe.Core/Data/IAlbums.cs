using fanaticServe.Core.Dto;

namespace fanaticServe.Core.Data;

public interface IAlbums
{
    /// <summary>
    /// 全てのアルバムの一覧を取得する
    /// </summary>
    /// <remarks>Albums</remarks>
    public IEnumerable<ShowableAlbum> GetAllAlbums(string sortOrder, string searchString);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ArticleAlbum GetAlbumGroup(Guid id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ArticleAlbum> GetAlbumArticles(string sortOrder, string searchString);

    /// <summary>
    /// アルバムの詳細情報を1件取得する
    /// </summary>
    /// <remarks>Albums/Details</remarks>
    public DetailAlbum GetDetailAlbum(Guid id);
}
