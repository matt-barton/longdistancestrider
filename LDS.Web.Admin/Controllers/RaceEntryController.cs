using LDS.Data;
using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.Caching;
using LDS.Web.Admin.ViewModels;
using LDS.Web.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LDS.Web.Admin.Controllers;

[Route("RaceEntry")]
public class RaceEntryController(
    IRaceService raceService,
    IRunnerService runnerService,
    IRaceEntryService raceEntryService,
    ICacheInvalidation cacheInvalidation)
    : Controller
{
    [HttpGet("Edit/{raceId}/{runnerId}")]
    public IActionResult Edit(int raceId, int runnerId)
    {
        var race = raceService.Get(raceId);
        if (race == null)
        {
            return NotFound("Race not found");
        }
        
        var runner = runnerService.Get(runnerId);
        if (runner == null)
        {
            return NotFound("Runner not found");
        }
        
        var raceEntry = raceEntryService.Get(raceId, runnerId);
        if (raceEntry == null)
        {
            return NotFound("No race entry found for this runner/race");
        }

        return View(new RaceEntryViewModel
        {
            Miles =  raceEntry.Miles,
            RunnerId =  runner.Id,
            RunnerName = runner.FullName,
            RaceId = raceEntry.RaceId,
            RaceName = race.Name
        });
    }
    
    [HttpPost("Edit/{raceId}/{runnerId}")]
    public async Task<IActionResult> Edit (RaceEntryViewModel raceEntry)
    {
        if (!ModelState.IsValid)
        {
            return View(raceEntry);
        }

        if (raceEntry.RaceId == null || raceEntry.RunnerId == null)
        {
            return BadRequest("Race and Runner must be specified");
        }

        var race = raceService.Get(raceEntry.RaceId.Value);
        if (race == null)
        {
            return NotFound("Race not found");
        }
        
        var runner = runnerService.Get(raceEntry.RunnerId.Value);
        if (runner == null)
        {
            return NotFound("Runner not found");
        }

        raceEntry.ChangesSaved = await raceEntryService.UpdateMiles(raceEntry.RaceId.Value, raceEntry.RunnerId.Value, raceEntry.Miles);
        cacheInvalidation.Invalidate([new CacheInvalidationDetail
        {
            Action = CacheInvalidationAction.EditRaceEntry,
            RunnerId = raceEntry.RunnerId.ToString(),
            RaceId = raceEntry.RaceId.ToString(),
            Gender = runner.Gender,
            Year = race.Date!.Value.Year.ToString()
        }]);
        
        return View(raceEntry);
    }

}