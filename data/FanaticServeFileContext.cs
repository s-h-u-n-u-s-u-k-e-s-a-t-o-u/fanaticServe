using fanaticServe.Models;
using System.Reflection;
using System.Text;

namespace fanaticServe.Data;

public class FanaticServeFileContext : IFanaticServeContext
{
    private readonly string _dataFolder;

    public FanaticServeFileContext(string contentRootPath)
    {
        _dataFolder = Path.Combine(contentRootPath, "DataSource");
        // 初期読み込み（必要なら遅延に変更可能）
        Abstract_albums = LoadFile<Abstract_album>("Abstract_album").AsQueryable();
        Abstract_Album_Notes = LoadFile<Abstract_Album_Note>("Abstract_Album_Note").AsQueryable();
        Abstract_album_links = LoadFile<Abstract_album_link>("Abstract_album_link").AsQueryable();
        Albums = LoadFile<Album>("Album").AsQueryable();
        Album_Notes = LoadFile<Album_Note>("Album_Note").AsQueryable();

        MediaTypes = LoadFile<MediaType>("Media").AsQueryable();
        Labels = LoadFile<Label>("Label").AsQueryable();
        Tracks = LoadFile<Track>("Track").AsQueryable();

        Abstract_events = LoadFile<Abstract_event>("Abstract_event").AsQueryable();
        Abstract_event_links = LoadFile<Abstract_event_link>("Abstract_event_link").AsQueryable();
        LiveEvents = LoadFile<LiveEvent>("Live_Event").AsQueryable();
        Live_Event_Notes = LoadFile<Live_Event_Note>("Live_Event_Note").AsQueryable();
        Set_lists = LoadFile<Set_list>("Set_list").AsQueryable();
        Set_List_Notes = LoadFile<Set_List_Note>("Set_List_Note").AsQueryable();
        Songs = LoadFile<Song>("Song").AsQueryable();
    }

    public IQueryable<Abstract_album> Abstract_albums { get; }
    public IQueryable<Abstract_Album_Note> Abstract_Album_Notes { get; }   
    public IQueryable<Abstract_album_link> Abstract_album_links { get; }
    public IQueryable<Album> Albums { get; }
    public IQueryable<Album_Note> Album_Notes { get; }    

    public IQueryable<MediaType> MediaTypes { get; }
    public IQueryable<Label> Labels { get; }
    public IQueryable<Track> Tracks { get; }

    public IQueryable<Abstract_event> Abstract_events { get; }
    public IQueryable<Abstract_event_link> Abstract_event_links { get; }
    public IQueryable<LiveEvent> LiveEvents { get; }
    public IQueryable<Live_Event_Note> Live_Event_Notes { get; }
    public IQueryable<Set_list> Set_lists { get; }
    public IQueryable<Set_List_Note> Set_List_Notes { get; }

    public IQueryable<Song> Songs { get; }


    private List<T> LoadFile<T>(string fileName) where T : new()
    {
        var result = new List<T>();
        var path = Path.Combine(_dataFolder, $"{fileName}.dat");
        if (!File.Exists(path)) return result;

        // UTF-8 で読み込み。ファイルはヘッダー無し、タブ区切りを前提とする
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        if (lines.Length == 0) return result;

        char delimiter = '\t';

        // プロパティは宣言順を想定して MetadataToken でソート
        var props = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .OrderBy(p => p.MetadataToken)
            .ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            var cols = line.Split(delimiter);
            var obj = new T();

            int max = Math.Min(cols.Length, props.Length);
            for (int c = 0; c < max; c++)
            {
                var prop = props[c];
                var raw = cols[c].Trim();
                if (string.IsNullOrEmpty(raw)) continue;

                try
                {
                    object? val = ConvertToType(raw, prop.PropertyType);
                    // null を非 nullable 値型に設定しない（既定値を保持）
                    if (val == null && prop.PropertyType.IsValueType && Nullable.GetUnderlyingType(prop.PropertyType) == null)
                    {
                        continue;
                    }
                    prop.SetValue(obj, val);
                }
                catch
                {
                    // パース失敗は無視（必要ならログ追加）
                }
            }
            result.Add(obj);
        }

        return result;
    }

    private static object? ConvertToType(string raw, Type targetType)
    {
        var nullableType = Nullable.GetUnderlyingType(targetType);
        var t = nullableType ?? targetType;

        if (t == typeof(Guid))
            return Guid.TryParse(raw, out var g) ? g : (object?)null;
        if (t == typeof(int))
            return int.TryParse(raw, out var i) ? i : (object?)null;
        if (t == typeof(long))
            return long.TryParse(raw, out var l) ? l : (object?)null;
        if (t == typeof(DateTime))
            return DateTime.TryParse(raw, out var dt) ? dt : (object?)null;
        if (t == typeof(TimeSpan))
            return TimeSpan.TryParse(raw, out var ts) ? ts : (object?)null;
        if (t == typeof(bool))
            return bool.TryParse(raw, out var b) ? b : (object?)null;
        if (t.IsEnum)
        {
            try { return Enum.Parse(t, raw); } catch { return null; }
        }
        // それ以外は文字列
        if (t == typeof(string)) return raw;
        try
        {
            return Convert.ChangeType(raw, t);
        }
        catch
        {
            return null;
        }
    }
}