using LDS.Data.Services.Interfaces;
using LDS.Web.Public.Extensions;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers;

[Route("Leaderboard")]
public class LeaderboardController (ITotalMilesService totalMilesService, IParametersService parametersService) : Controller
{
    // GET
    [HttpGet("{gender}")]
    public IActionResult Index(string gender)
    {
        var runners = totalMilesService.GetLeaderboard(gender)
            .Select(r => new LeaderboardRunnerViewModel
            {
                Name = r.FirstName + " " + r.LastName,
                Id = r.RunnerId,
                Miles = r.Miles
            })
            .ToList()
            .WithPositions();
            
        var year = parametersService.GetCurrentYear();
            
        var lastUpdated = parametersService.GetLastUpdated();

        return View(new LeaderboardGenderViewModel
        {
            Gender = gender,
            Year = year.ToString(),
            LastUpdated = lastUpdated,
            Runners = runners
        });
    }
}