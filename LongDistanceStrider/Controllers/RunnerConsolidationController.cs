using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace LongDistanceStrider.Controllers
{
    [Route("RunnerConsolidation")]
    public class RunnerConsolidationController : Controller
    {

        private LDSContext _ldsContext;

        public RunnerConsolidationController(LDSContext ldsContext)
        {
            _ldsContext = ldsContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(InitialiseViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index (RunnerConsolidationViewModel consolidation)
        {
            if (consolidation.RunnerToKeepId == consolidation.RunnerToRemoveId)
            {
                ModelState.AddModelError(nameof(consolidation.RunnerToRemove), "Runners to keep and remove must be different.");
            }

            if (!ModelState.IsValid)
            {
                return View(InitialiseViewModel(consolidation));
            }

            var entriesToMove = _ldsContext.RacePartipation
                .Where(r => r.RunnerId == consolidation.RunnerToRemoveId)
                .ToList();

            var sql = @"UPDATE [RaceEntry]
                           SET RunnerId = @RunnerToKeepId
                         WHERE RunnerId = @RunnerToRemoveId";

            await _ldsContext.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@RunnerToKeepId", consolidation.RunnerToKeepId),
                new SqlParameter("@RunnerToRemoveId", consolidation.RunnerToRemoveId)
            );

            var runnerToKeep = _ldsContext.Runners
                .FirstOrDefault(r => r.Id == consolidation.RunnerToKeepId);

            var runnerToRemove = _ldsContext.Runners
                .FirstOrDefault(r => r.Id == consolidation.RunnerToRemoveId);

            _ldsContext.Runners.Remove(runnerToRemove);

            if (consolidation.CreateAlias)
            {
                var alias = new RunnerAlias
                {
                    RunnerId = consolidation.RunnerToKeepId,
                    Alias = runnerToRemove.FullName
                };
                _ldsContext.Add<RunnerAlias>(alias);
            }

            _ldsContext.SaveChanges();

            var model = new RunnerConsolidationViewModel
            {
                RemovedRunnerId = runnerToRemove.Id,
                RemovedRunnerName = runnerToRemove.FullName,
                KeptRunnerId = runnerToKeep.Id,
                KeptRunnerName = runnerToKeep.FullName,
                AliasCreated = consolidation.CreateAlias,
                EntriesMoved = [.. entriesToMove.Select(e => new RunnerRaceEntryViewModel
                {
                    RaceName = e.RaceName,
                    Date = e.Date,
                    Miles = e.Miles
                })]
            };

            return View(InitialiseViewModel(model));
        }

        private RunnerConsolidationViewModel InitialiseViewModel (RunnerConsolidationViewModel? model = null)
        {
            var runners = _ldsContext.Runners
                .OrderBy(r => r.LastName)
                .ThenBy(r => r.FirstName)
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