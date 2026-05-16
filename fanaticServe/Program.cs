// Import the Azure.Monitor.OpenTelemetry.AspNetCore namespace.

using fanaticServe.Core.Data;
using fanaticServe.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ログの設定
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add OpenTelemetry and configure it to use Azure Monitor.
// APPLICATIONINSIGHTS_CONNECTION_STRING という名前で環境変数を設定し、UseAzureMonitor()を呼び出せば自動的に認識されます。
// builder.Services.AddOpenTelemetry().UseAzureMonitor();

// Add services to the container.

builder.Configuration.AddEnvironmentVariables(prefix: "SQLCONNSTR_SSSWare_");
var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_SSSWare_ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<fanaticServe.Data.FanaticServeContext>(options =>
    options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));


// インターフェイスを DbContext にマップする
builder.Services.AddScoped<IFanaticServeContext>(provider => provider.GetRequiredService<FanaticServeContext>());


builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

// データベース例外フィルター
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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