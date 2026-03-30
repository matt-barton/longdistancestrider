using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace LDS.Web.Admin.Controllers
{
    [Route("RunnerConsolidation")]
    public class RunnerConsolidationController(
        IRunnerService runnerService,
        IRaceParticipationService raceParticipationService,
        IRaceEntryService raceEntryService) : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View(InitialiseViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RunnerConsolidationViewModel consolidation)
        {
            if (consolidation.RunnerToKeepId == consolidation.RunnerToRemoveId)
            {
                ModelState.AddModelError(nameof(consolidation.RunnerToRemove),
                    "Runners to keep and remove must be different.");
            }

            if (!ModelState.IsValid)
            {
                return View(InitialiseViewModel(consolidation));
            }

            var entriesToMove = raceParticipationService.GetForRunner(consolidation.RunnerToRemoveId);

            await raceEntryService.UpdateRunner(consolidation.RunnerToRemoveId, consolidation.RunnerToKeepId);

            var runnerToKeep = runnerService.Get(consolidation.RunnerToKeepId);
            if (runnerToKeep == null)
            {
                return BadRequest("Runner keep must be specified.");
            }

            var runnerToRemove = runnerService.Get(consolidation.RunnerToRemoveId);
            if (runnerToRemove == null)
            {
                return BadRequest("Runner keep must be specified.");
            }
            
            runnerService.Delete(runnerToRemove);

            if (consolidation.CreateAlias)
            {
                runnerService.CreateAlias(consolidation.RunnerToKeepId, runnerToRemove.FullName);
            }

            var model = new RunnerConsolidationViewModel
            {
                RemovedRunnerId = runnerToRemove.Id,
                RemovedRunnerName = runnerToRemove.FullName,
                KeptRunnerId = runnerToKeep.Id,
                KeptRunnerName = runnerToKeep.FullName,
                AliasCreated = consolidation.CreateAlias,
                EntriesMoved =
                [
                    .. entriesToMove.Select(e => new RunnerRaceEntryViewModel
                    {
                        RaceName = e.RaceName,
                        Date = e.Date,
                        Miles = e.Miles
                    })
                ]
            };

            return View(InitialiseViewModel(model));
        }

        private RunnerConsolidationViewModel InitialiseViewModel (RunnerConsolidationViewModel? model = null)
        {
            var runners = runnerService
                .GetAll()
                .ToList();

            if (model == null)
            {
                model = new RunnerConsolidationViewModel();
            }

            model.RunnerToKeep = runners;
            model.RunnerToRemove = runners;

            return model;
        }
    }
}