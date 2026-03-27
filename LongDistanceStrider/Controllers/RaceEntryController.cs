using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LongDistanceStrider.Controllers;

[Route("RaceEntry")]
public class RaceEntryController(LDSContext ldsContext) : Controller
{
    [HttpGet("Edit/{raceId}/{runnerId}")]
    public IActionResult Edit(int raceId, int runnerId)
    {
        var race = ldsContext.Races.FirstOrDefault(x => x.Id == raceId);
        if (race == null)
        {
            return NotFound("Race not found");
        }
        
        var runner = ldsContext.Runners.FirstOrDefault(x => x.Id == runnerId);
        if (runner == null)
        {
            return NotFound("Runner not found");
        }
        
        var raceEntry = ldsContext.RaceEntries.FirstOrDefault(x => x.RaceId == raceId && x.RunnerId == runnerId);
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

        const string sql = @"UPDATE [RaceEntry]
                                SET Miles = @Miles
                              WHERE RaceId = @RaceId
                                AND RunnerId = @RunnerId";

        await ldsContext.Database.ExecuteSqlRawAsync(
            sql,
            new SqlParameter("@RaceId", raceEntry.RaceId),
            new SqlParameter("@RunnerId", raceEntry.RunnerId),
            new SqlParameter("@Miles", raceEntry.Miles)
        );

        raceEntry.ChangesSaved = true;

        return View(raceEntry);
    }

}