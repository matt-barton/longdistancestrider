using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.Extensions;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers
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

            var races = raceParticipationService.GetForRunner(runner.Id);
            
            var model = new RunnerViewModel
            {
                DisplayName = runner.FullName,
                Id = runner.Id,
                Gender = runner.Gender,
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

        [HttpGet("Edit/{Id}")]
        public IActionResult Edit(int? id)
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

            return View(new RunnerViewModel(runner));
        }

        [HttpPost("Edit/{runner}")]
        public async Task<IActionResult> Edit (RunnerViewModel runner)
        {
            if (!ModelState.IsValid)
            {
                return View(runner);
            }

            if (runner.FirstName == null || runner.LastName == null ||  runner.Gender == null)
            {
                return View(runner);    
            }
            
            runner.ChangesSaved = await runnerService.Update(runner.Id, runner.FirstName, runner.LastName, runner.Gender);;

            return View(runner);
        }

        [HttpGet("List")]
        public IActionResult List ()
        {
            var runners = runnerService.GetAll()
                .Select(r => new RunnerViewModel
                {
                    DisplayName = r.FullName,
                    Id = r.Id,
                    Gender = r.Gender
                })
                .ToList();

            return View(runners);
        }

        [HttpGet("List/Gender")]
        public IActionResult ListGender ()
        {
            var runners = runnerService.GetAllMissingGender()
                .Select(r => new RunnerViewModel
                {
                    DisplayName = r.FullName,
                    Id = r.Id,
                    Gender = r.Gender
                })
                .ToList();

            return View("List", runners);
        }

        [HttpGet("Leaderboard")]
        public IActionResult Leaderboard ()
        {
            var maleRunners = totalMilesService.GetLeaderboard("M")
                .Select(r => new LeaderboardRunnerViewModel
                {
                    Name = r.FirstName + " " + r.LastName,
                    Id = r.RunnerId,
                    Miles = r.Miles
                })
                .ToList()
                .WithPositions();
            
            var femaleRunners = totalMilesService.GetLeaderboard("F")
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
                Year = year.ToString(),
                LastUpdated = lastUpdated,
                Women = femaleRunners,
                Men = maleRunners
            });
        }
    }
}
