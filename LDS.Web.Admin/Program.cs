using GovUk.Frontend.AspNetCore;
using LDS.Data;
using LDS.Web.Shared.Extensions;
using LDS.Web.Admin;
using LDS.Web.Admin.Caching;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_AUTH_CLIENT_ID") ?? throw new ApplicationException("Google auth client Id not set");
        options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_AUTH_CLIENT_SECRET") ?? throw new ApplicationException("Google auth client secret not set");
    });
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddRazorPages();
builder.Logging.AddConsole();
builder.Services.AddTransient<ICacheInvalidation, CacheInvalidation>();
builder.AddLdsDataServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseGovUkFrontend();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapStaticAssets().Add(endpointBuilder => 
    endpointBuilder.Metadata.Add(new AllowAnonymousAttribute())
);
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.UseAntiforgery();
app.UseStatusCodePages();
app.Run();