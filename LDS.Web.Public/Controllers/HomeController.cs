using LDS.Data.Services.Interfaces;
using LDS.Web.Public.Caching;
using LDS.Web.Public.Extensions;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Controllers;

public class HomeController(
    ILeaderboardCache leaderboardCache,
    IParameterCache parameterCache,
    IMemoryCache cache) : Controller
{
    // GET
    public IActionResult Index()
    {
        var maleRunners = leaderboardCache.GetLeaderboardRunners("M", 10);
        var femaleRunners = leaderboardCache.GetLeaderboardRunners("F", 10);
        var year = parameterCache.GetCurrentYear();
        var lastUpdated = parameterCache.GetLastUpdated();

        return View(new LeaderboardViewModel
        {
            Year = year.ToString(),
            LastUpdated = lastUpdated,
            Women = femaleRunners,
            Men = maleRunners
        });
    }

    [HttpGet("/About")]
    public IActionResult About ()
    {
        return View();
    }
    
    public IActionResult Error ()
    {
        return View();
    }
}