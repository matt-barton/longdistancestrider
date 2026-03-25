using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace LongDistanceStrider.Controllers
{
    [Route("Race")]
    public class RaceController : Controller
    {
        private LDSContext _ldsContext;

        public RaceController(LDSContext ldsContext)
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

            var race = _ldsContext.Races.Where(r => r.Id == Id).FirstOrDefault();

            if (race == null)
            {
                return new NotFoundResult();    
            }

            var entries = _ldsContext.RacePartipation
                .Where(r => r.RaceId == Id)
                .OrderByDescending(r => r.Miles)
                .ThenBy(r => r.RunnerName)
                .ToList();

            var model = new RaceViewModel
            {
                Name = race.Name,
                Id = race.Id,
                Date = race.Date,
                Entries = entries.Select(re => new RaceEntryViewModel()
                {
                    RunnerName = re.RunnerName,
                    RunnerId = re.RunnerId,
                    Miles = re.Miles
                }).ToList()
            };
            return View(model);
        }

/*
        [HttpGet("Edit/{Id}")]
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
            {
                return new NotFoundResult();    
            }

            var runner = _ldsContext.Runners
                .Where(r => r.Id == Id)
                .FirstOrDefault();

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
*/
    }
}
