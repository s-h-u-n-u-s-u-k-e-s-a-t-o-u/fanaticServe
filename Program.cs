using fanaticServe.Data;
using Microsoft.EntityFrameworkCore;
// Import the Azure.Monitor.OpenTelemetry.AspNetCore namespace.
using Azure.Monitor.OpenTelemetry.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// ログの設定
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

// Add OpenTelemetry and configure it to use Azure Monitor.
// APPLICATIONINSIGHTS_CONNECTION_STRING という名前で環境変数を設定し、UseAzureMonitor()を呼び出せば自動的に認識されます。
builder.Services.AddOpenTelemetry().UseAzureMonitor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database を使わない -> DbContext 登録を削除し、ファイルコンテキストを登録する
// 既定では ContentRoot/DataSource/*.dat を読み込みます
builder.Services.AddSingleton<IFanaticServeContext>(sp =>
    new FanaticServeFileContext(builder.Environment.ContentRootPath));

// データベース例外フィルターは不要なのでコメントアウト（必要なら別処理）
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();