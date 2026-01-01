namespace fanaticServe.Core.Dto;

public class DashBoard
{
    /// <summary>
    /// 最近更新したイベントデータ
    /// </summary>
    public List<DetailEvent> RecentlyChangedEvents { get; set; }= new List<DetailEvent>();

    /// <summary>
    /// 最近開催したイベント
    /// </summary>
    public List<DetailEvent> RecentLiveEvents { get; set; } = new List<DetailEvent>();
}
