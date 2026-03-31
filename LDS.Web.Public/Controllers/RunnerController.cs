using LDS.Data.Services.Interfaces;
using LDS.Web.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Public.Controllers
{
    [Route("Runner")]
    public class RunnerController(
        IRunnerService runnerService,
        IRaceParticipationService raceParticipationService,
        ITotalMilesService totalMilesService,
        IParametersService parametersService) : Controller
    {
        [HttpGet("{Id}")]
        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return new NotFoundResult();    
            }

            var runner = runnerService.Get(id.Value);
            
            if (runner == null)
            {
                return new NotFoundResult();    
            }

            var year = parametersService.GetCurrentYear();
            
            var races = raceParticipationService.GetForRunner(runner.Id, year);
            
            var model = new RunnerViewModel
            {
                DisplayName = runner.FullName,
                Id = runner.Id,
                Gender = runner.Gender,
                Year = year.ToString(),
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
