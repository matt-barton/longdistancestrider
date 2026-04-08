using GovUk.Frontend.AspNetCore;
using LDS.Data;
using LDS.Data.Configuration;
using LDS.Web.Public;
using LDS.Web.Public.Caching;
using LDS.Web.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.SetDefaultCulture("en-GB")
        .AddSupportedCultures(["en-GB"])
        .AddSupportedUICultures(["en-GB"]);
});
builder.Services.AddDbContext<LdsContext>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend(options => options.Rebrand = true);

builder.Services.AddTransient<IParameterCache, ParameterCache>();
builder.Services.AddTransient<ILeaderboardCache, LeaderboardCache>();
builder.Services.AddTransient<IRaceCache, RaceCache>();
builder.Services.AddTransient<IRaceParticipationCache, RaceParticipationCache>();
builder.Services.AddTransient<IRunnerCache, RunnerCache>();
builder.Services.AddTransient<ICacheInvalidation, CacheInvalidation>();

builder.AddLdsDataServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
