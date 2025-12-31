using fanaticServe.Core.Dto;

namespace fanaticServe.Core.Data;

public interface IEvents
{
    /// <summary>
    /// 全てのライブ/イベントの一覧を取得する
    /// </summary>
    /// <remarks>Events</remarks>
    public IEnumerable<ShowableEvent> GetAllEvents(string sortOrder);

    /// <summary>
    /// 全てのセットリストの一覧を取得する
    /// </summary>
    /// <remarks>Events/Articles</remarks>
    public IEnumerable<ArticleEvent> GetAllEventArticles(string sortOrder, string searchString);

    /// <summary>抽象イベント:EventGroupとそれに紐づくイベントのセットリストを取得する</summary>
    /// <remarks>Events/EventGroup/{id} </remarks>
    /// <param name="abstId">抽象イベントID</param>
    public ArticleEvent GetEventGroup(Guid abstId, string sortOrder);

    /// <summary>ライブ/イベントのセットリストを1件取得する</summary>
    /// <remarks>Events/Details/{id} </remarks>
    /// <param name="eventId">イベントID</param>
    public DetailEvent GetDetailEvent(Guid eventId);
}
