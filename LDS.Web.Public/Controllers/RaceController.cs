using LDS.Web.Public.Caching;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers;

[Route("/Race")]
public class RaceController (
    IRaceCache raceCache,
    IParameterCache parametersCache,
    IRaceParticipationCache raceParticipationCache) : Controller
{
    [HttpGet("All/{displayYear:int?}")]
    public IActionResult DisplayList(int? displayYear)
    {
        var currentYear = parametersCache.GetCurrentYear();
        
        displayYear ??= currentYear;

        var races = raceCache.GetAll(displayYear.Value)
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.Name);
        
        var firstYear = parametersCache.GetFirstYear();

        return View(new RaceListViewModel
        {
            Year = displayYear.Value,
            PreviousYear = firstYear < displayYear
                ? displayYear - 1
                : null,
            NextYear = displayYear < currentYear
                ? displayYear + 1
                : null,
            Races = races.Select(r => new RaceViewModel
            {
                Id = r.Id,
                Date = r.Date!.Value,
                Name = r.Name!
            }).ToList()
        });
    }

    [HttpGet("{raceId:int}")]
    public IActionResult DisplaySingle(int raceId)
    {
        var race = raceCache.Get(raceId);

        if (race == null) return NotFound();

        var runners = raceParticipationCache
            .GetForRace(raceId)
            .OrderByDescending(r => r.Miles)
            .ThenBy(r => r.RunnerLastName)
            .ThenBy(r => r.RunnerName);
        
        return View(new RaceViewModel
        {
            Id = race.Id,
            Name = race.Name!,
            Date = race.Date!.Value,
            Runners = runners.Select(r => new RaceRunnerViewModel
            {
                Id = r.RunnerId,
                Name = r.RunnerName!,
                Miles = r.Miles
            })
        });
    }
}