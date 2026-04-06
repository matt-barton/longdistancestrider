using LDS.Web.Public.Caching;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers
{
    [Route("Runner")]
    public class RunnerController(
        IRunnerCache runnerCache,
        IRaceParticipationCache raceParticipationCache,
        IParameterCache parameterCache) : Controller
    {
        [HttpGet("{Id:int?}/{displayYear:int?}")]
        public IActionResult Index(int? id, int? displayYear)
        {
            if (!id.HasValue)
            {
                return new NotFoundResult();    
            }

            var runner = runnerCache.Get(id.Value);
            
            if (runner == null)
            {
                return new NotFoundResult();    
            }

            var currentYear = parameterCache.GetCurrentYear();

            displayYear ??= currentYear;
            
            var races = raceParticipationCache.GetForRunner(runner.Id, displayYear.Value);
            
            var firstYear = parameterCache.GetFirstYear();

            var model = new RunnerViewModel
            {
                DisplayName = runner.FullName,
                Id = runner.Id,
                Gender = runner.Gender,
                Year = displayYear.Value,
                PreviousYear = firstYear < displayYear
                    ? displayYear - 1
                    : null,
                NextYear = displayYear < currentYear
                    ? displayYear + 1
                    : null,
                RaceEntries = races.Select(re => new RunnerRaceEntryViewModel()
                {
                    RaceName = re.RaceName,
                    RaceId = re.RaceId,
                    Date = re.Date,
                    Miles = re.Miles
                }).ToList()
            };
            return View(model);
        }
    }
}
