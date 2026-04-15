using GovUk.Frontend.AspNetCore;
using LDS.Data;
using LDS.Web.Shared.Extensions;
using LDS.Web.Admin;
using LDS.Web.Admin.Caching;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.SetDefaultCulture("en-GB")
        .AddSupportedCultures(["en-GB"])
        .AddSupportedUICultures(["en-GB"]);
});

builder.Services.AddDbContext<LdsContext>(o => o
    .UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
//builder.Services.AddGovUkFrontend(options => options.Rebrand = true);
builder.Services.AddGovUkFrontend();

builder.Logging.AddConsole();

builder.Services.AddTransient<ICacheInvalidation, CacheInvalidation>();

builder.AddLdsDataServices();

var app = builder.Build();

/*
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
*/
app.UseHttpsRedirection();
app.UseGovUkFrontend();

app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAntiforgery();
app.UseStatusCodePages();
app.Run();

/*
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/
