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

// var aiConnStr = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
// Console.WriteLine($"[DEBUG] Azure Monitor接続文字列: {aiConnStr}");

builder.Services.AddOpenTelemetry().UseAzureMonitor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddEnvironmentVariables(prefix: "SQLCONNSTR_SSSWare_");
var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_SSSWare_ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<FanaticServeContext>(options =>
    options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));

// データベース例外フィルター
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

