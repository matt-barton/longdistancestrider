using LDS.Data.Services.Interfaces;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers;

[Route("/Race")]
public class RaceController (IRaceService raceService, IParametersService parametersService, IRaceParticipationService raceParticipationService) : Controller
{
    [HttpGet("All")]
    public IActionResult DisplayList()
    {
        var year = parametersService.GetCurrentYear();

        var races = raceService
            .GetAll(year)
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.Name);
        
        return View(new RaceListViewModel
        {
            Year = year,
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
        var race = raceService.Get(raceId);

        if (race == null) return NotFound();

        var runners = raceParticipationService
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