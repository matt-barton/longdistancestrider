using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using LDS.Web.Public.Extensions;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers;

[Route("Leaderboard")]
public class LeaderboardController (ITotalMilesService totalMilesService, IParametersService parametersService, IRaceParticipationService raceParticipationService) : Controller
{
    // GET
    [HttpGet("{gender}/{displayYear:int?}")]
    public IActionResult Index(string gender, int? displayYear)
    {
        var currentYear = parametersService.GetCurrentYear();

        displayYear ??= currentYear;
        
        IQueryable<TotalMilesBase> queryable;
        switch (displayYear)
        {
            case 2026: queryable = totalMilesService.GetLeaderboard(gender); break;
            case 2025: queryable = totalMilesService.GetLeaderboard2025(gender); break;
            default: return NotFound("Unknown year " + displayYear);
        }

        var runners = queryable.Select(r => new LeaderboardRunnerViewModel
            {
                Name = r.FirstName + " " + r.LastName,
                Id = r.RunnerId,
                Miles = r.Miles
            })
            .ToList()
            .WithPositions();

        var firstYear = parametersService.GetFirstYear();
        var lastUpdated = parametersService.GetLastUpdated();

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
} 