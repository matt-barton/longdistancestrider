using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using LDS.Web.Public.Caching;
using LDS.Web.Public.Extensions;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Controllers;

[Route("Leaderboard")]
public class LeaderboardController (
    ITotalMilesService totalMilesService,
    ILeaderboardCache leaderboardCache,
    IParameterCache parameterCache,
    IMemoryCache cache) : Controller
{
    // GET
    [HttpGet("{gender}/{displayYear:int?}")]
    public IActionResult Index(string gender, int? displayYear)
    {
        var currentYear = parameterCache.GetCurrentYear();

        displayYear ??= currentYear;

        try
        {
            var runners = leaderboardCache.GetYearLeaderboard (gender, displayYear.Value);
            var firstYear = parameterCache.GetFirstYear();
            var lastUpdated = parameterCache.GetLastUpdated();

            return View(new LeaderboardGenderViewModel
            {
                Gender = gender,
                Year = displayYear.Value,
                PreviousYear = firstYear < displayYear
                    ? displayYear - 1
                    : null,
                NextYear = displayYear < currentYear
                    ? displayYear + 1
                    : null,
                StandingsType = displayYear == currentYear
                    ? "Current"
                    : "Final",
                LastUpdated = displayYear == currentYear
                    ? lastUpdated
                    : null,
                Runners = runners

            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 