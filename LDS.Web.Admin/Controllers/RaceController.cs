using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers
{
    [Route("Race")]
    public class RaceController(IRaceService raceService, IRaceParticipationService raceParticipationService) : Controller
    {
        [HttpGet("{Id}")]
        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return new NotFoundResult();    
            }

            var race = raceService.Get(id.Value);

            if (race == null)
            {
                return new NotFoundResult();    
            }

            var entries = raceParticipationService.GetForRace(race.Id);

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
    }
}
