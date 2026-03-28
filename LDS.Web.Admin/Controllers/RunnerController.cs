using LDS.Web.Admin.Extensions;
using LDS.Web.Admin.Models;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LDS.Web.Admin.Controllers
{
    [Route("Runner")]
    public class RunnerController : Controller
    {
        private LDSContext _ldsContext;

        public RunnerController(LDSContext ldsContext)
        {
            _ldsContext = ldsContext;
        }

        [HttpGet("{Id}")]
        public IActionResult Index(int? Id)
        {
            if (!Id.HasValue)
            {
                return new NotFoundResult();    
            }

            var runner = _ldsContext.Runners.Where(r => r.Id == Id).FirstOrDefault();

            if (runner == null)
            {
                return new NotFoundResult();    
            }

            var races = _ldsContext.RacePartipation
                .Where(r => r.RunnerId == Id)
                .OrderBy(r => r.Date)
                .ToList();

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
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
            {
                return new NotFoundResult();    
            }

            var runner = _ldsContext.Runners
                .FirstOrDefault(r => r.Id == Id);

            if (runner == null)
            {
                return new NotFoundResult();    
            }

            return View(new RunnerViewModel(runner));
        }

        [HttpPost("Edit/{Id}")]
        public async Task<IActionResult> Edit (RunnerViewModel runner)
        {
            if (!ModelState.IsValid)
            {
                return View(runner);
            }

            var sql = @"UPDATE [Runner]
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Gender = @Gender
                        WHERE Id = @Id";

            await _ldsContext.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@FirstName", runner.FirstName),
                new SqlParameter("@LastName", runner.LastName),
                new SqlParameter("@Gender", runner.Gender),
                new SqlParameter("@Id", runner.Id)
            );

            runner.ChangesSaved = true;

            return View(runner);
        }

        [HttpGet("List")]
        public IActionResult List ()
        {
            var runners = _ldsContext.Runners
                .OrderBy(r => r.LastName)
                .ThenBy(r => r.FirstName)
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
            var runners = _ldsContext.Runners
                .Where(r => r.Gender == null || String.IsNullOrEmpty(r.Gender))
                .OrderBy(r => r.LastName)
                .ThenBy(r => r.FirstName)
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
            var maleRunners = _ldsContext.TotalMiles
                .Where(r => r.Gender == "M")
                .OrderByDescending(r => r.Miles)
                .ThenBy(r => r.LastName)
                .ThenBy(r => r.FirstName)
                .Select(r => new LeaderboardRunnerViewModel
                {
                    Name = r.FirstName + " " + r.LastName,
                    Id = r.RunnerId,
                    Miles = r.Miles
                })
                .ToList()
                .WithPositions();
            
            var femaleRunners = _ldsContext.TotalMiles
                .Where(r => r.Gender == "F")
                .OrderByDescending(r => r.Miles)
                .ThenBy(r => r.LastName)
                .ThenBy(r => r.FirstName)
                .Select(r => new LeaderboardRunnerViewModel
                {
                    Name = r.FirstName + " " + r.LastName,
                    Id = r.RunnerId,
                    Miles = r.Miles
                })
                .ToList()
                .WithPositions();

            var year = _ldsContext.Parameters
                .SingleOrDefault(p => p.Name == "CurrentYear");
            
            var lastUpdated = _ldsContext.Parameters
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
}
