using LongDistanceStrider.Components.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LongDistanceStrider.Controllers;

public class HomeController(LDSContext ldsContext) : Controller
{
    // GET
    public IActionResult Index()
    {
        var maleRunners = ldsContext.TotalMiles
            .Where(r => r.Gender == "M")
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.LastName)
            .ThenBy(r => r.FirstName)
            .Take(3)
            .Select(r => new LeaderboardRunnerViewModel
            {
                Name = r.FirstName + " " + r.LastName,
                Id = r.RunnerId,
                Miles = r.Miles
            })
            .ToList()
            .WithPositions();
            
        var femaleRunners = ldsContext.TotalMiles
            .Where(r => r.Gender == "F")
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.LastName)
            .ThenBy(r => r.FirstName)
            .Take(3)
            .Select(r => new LeaderboardRunnerViewModel
            {
                Name = r.FirstName + " " + r.LastName,
                Id = r.RunnerId,
                Miles = r.Miles
            })
            .ToList()
            .WithPositions();

        var year = ldsContext.Parameters
            .SingleOrDefault(p => p.Name == "CurrentYear");
            
        var lastUpdated = ldsContext.Parameters
            .SingleOrDefault(p => p.Name == "LastUpdated");

        return View(new LeaderboardViewModel
        {
            Year = year?.Value,
            LastUpdated = lastUpdated?.Value == null ? null : DateTime.Parse(lastUpdated?.Value),
            Women = femaleRunners,
            Men = maleRunners
        });
    }
}