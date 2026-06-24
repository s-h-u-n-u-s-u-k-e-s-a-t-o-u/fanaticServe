
using System.Diagnostics;

namespace fanaticServe.Middleware;

public class AccessLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AccessLoggingMiddleware> _logger;

    public AccessLoggingMiddleware(RequestDelegate next, ILogger<AccessLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var now = DateTime.Now;
        var dayOfWeek = GetJapaneseDayOfWeek(now.DayOfWeek);
        
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            
            var statusCode = context.Response.StatusCode;
            var path = context.Request.Path.Value;
            var method = context.Request.Method;
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            
            // ログ出力形式: タイムスタンプ | 曜日 | 時刻 | メソッド | パス | ステータスコード | 処理時間
            _logger.LogInformation(
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {DayOfWeek} | {Hour:D2}:{Minute:D2} | {Method} | {Path} | {StatusCode} | {ElapsedMs}ms",
                now,
                dayOfWeek,
                now.Hour,
                now.Minute,
                method,
                path,
                statusCode,
                elapsedMs
            );
        }
    }

    private static string GetJapaneseDayOfWeek(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Sunday => "日",
            DayOfWeek.Monday => "月",
            DayOfWeek.Tuesday => "火",
            DayOfWeek.Wednesday => "水",
            DayOfWeek.Thursday => "木",
            DayOfWeek.Friday => "金",
            DayOfWeek.Saturday => "土",
            _ => "?"
        };
    }
}