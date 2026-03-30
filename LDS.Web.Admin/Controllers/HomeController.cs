using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.Extensions;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers;

public class HomeController(ITotalMilesService totalMilesService, IParametersService parametersService) : Controller
{
    // GET
    public IActionResult Index()
    {
        var maleRunners = totalMilesService.GetLeaderboard("M")
            .Take(3)
            .Select(r => new LeaderboardRunnerViewModel
            {
                Name = r.FirstName + " " + r.LastName,
                Id = r.RunnerId,
                Miles = r.Miles
            })
            .ToList()
            .WithPositions();
            
        var femaleRunners = totalMilesService.GetLeaderboard("F")
            .Take(3)
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

        return View(new LeaderboardViewModel
        {
            Year = year,
            LastUpdated = lastUpdated,
            Women = femaleRunners,
            Men = maleRunners
        });
    }
}