using LDS.Data;
using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LDS.Web.Admin.Controllers;

[Route("RaceEntry")]
public class RaceEntryController(
    IRaceService raceService,
    IRunnerService runnerService,
    IRaceEntryService raceEntryService)
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
        
        raceEntry.ChangesSaved = await raceEntryService.UpdateMiles(raceEntry.RaceId.Value, raceEntry.RunnerId.Value, raceEntry.Miles);

        return View(raceEntry);
    }

}