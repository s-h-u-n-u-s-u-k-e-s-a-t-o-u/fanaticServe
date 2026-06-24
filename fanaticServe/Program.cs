// Import the Azure.Monitor.OpenTelemetry.AspNetCore namespace.
using Azure.Monitor.OpenTelemetry.AspNetCore;
using fanaticServe.Back.Data;
using fanaticServe.Core.Data;
using fanaticServe.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ログの設定 - IIS環境でも詳細情報を出力
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//builder.Logging.AddDebug();
//builder.Logging.AddEventLog();  // Windowsイベントログにも出力

// 例外ハンドリング
try
{
    // Add OpenTelemetry - エラーハンドリング
    if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")))
    {
        builder.Services.AddOpenTelemetry().UseAzureMonitor();
    }

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // ファイルコンテキスト初期化
    builder.Services.AddSingleton<IFanaticServeContext>(sp =>
    {
        var contentRoot = builder.Environment.ContentRootPath;
        Console.WriteLine($"ContentRootPath: {contentRoot}");
        Console.WriteLine($"DataSource path: {Path.Combine(contentRoot, "DataSource")}");
        return new FanaticServeFileContext(contentRoot);
    });

    // DI登録
    builder.Services.AddScoped<IAlbums, fanaticServe.Back.AlbumService>();
    builder.Services.AddScoped<IEvents, fanaticServe.Back.EventService>();
    builder.Services.AddScoped<IPeople, fanaticServe.Back.PeopleService>();
    builder.Services.AddScoped<ISongs, fanaticServe.Back.SongService>(); 
    builder.Services.AddScoped<IStarMatrix, fanaticServe.Back.StarMatrixService>(); 

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    // アクセスログミドルウェアを追加
    app.UseMiddleware<AccessLoggingMiddleware>();

    app.UseRouting();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal error: {ex}");
    throw;
}