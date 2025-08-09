using fanaticServe.Data;
using Microsoft.EntityFrameworkCore;
// Import the Azure.Monitor.OpenTelemetry.AspNetCore namespace.
using Azure.Monitor.OpenTelemetry.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// ���O�̐ݒ�
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

// Add OpenTelemetry and configure it to use Azure Monitor.
// APPLICATIONINSIGHTS_CONNECTION_STRING �Ƃ������O�Ŋ��ϐ���ݒ肵�AUseAzureMonitor()���Ăяo���Ύ����I�ɔF������܂��B

// var aiConnStr = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");
// Console.WriteLine($"[DEBUG] Azure Monitor�ڑ�������: {aiConnStr}");

builder.Services.AddOpenTelemetry().UseAzureMonitor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddEnvironmentVariables(prefix: "SQLCONNSTR_SSSWare_");
var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_SSSWare_ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<FanaticServeContext>(options =>
    options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure()));

// �f�[�^�x�[�X��O�t�B���^�[
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

