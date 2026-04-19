using System.Text.RegularExpressions;
using LDS.Data;
using LDS.Data.Exceptions;
using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using LDS.Web.Admin.Caching;
using LDS.Web.Admin.ViewModels;
using LDS.Web.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers;

[Route("RaceParticipation")]
public class RaceParticipationController(
    LdsContext ldsContext,
    ICacheInvalidation cacheInvalidation,
    IParametersService parametersService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new RaceParticipationViewModel
        {
            LastAddedRace = GetLastAddedRace(),
        };
        return View(viewModel);
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index ([Bind("Name","Date","Runners","Miles")] RaceParticipationViewModel raceParticipation)
    {
        if (!ModelState.IsValid || raceParticipation == null)
        {
            return View(raceParticipation);
        }
            
        string[] runnerNames = raceParticipation.Runners.Split(",");
        int raceId = FindOrCreateRace(raceParticipation.Name, DateOnly.Parse(raceParticipation.Date));
        var cacheInvalidations = new List<CacheInvalidationDetail>();
        var raceEntryCreated = false;
        
        foreach (string name in runnerNames)
        {
            try
            {
                decimal miles = raceParticipation.Miles;
                string? gender = null;
                string runnerName = name;

                var extractedRunnerData = ExtractRunnerData(runnerName, RegexType.NameAndGender);
                if (extractedRunnerData.Success)
                {
                    runnerName = extractedRunnerData.Name;
                    gender = extractedRunnerData.Gender;
                }
                else
                {
                    extractedRunnerData = ExtractRunnerData(runnerName, RegexType.NameAndMiles);
                    if (extractedRunnerData.Success)
                    {
                        runnerName = extractedRunnerData.Name;
                        miles = extractedRunnerData.Miles;
                    }
                    else
                    {
                        extractedRunnerData = ExtractRunnerData(runnerName, RegexType.NameAndGenderAndMiles);
                        if (extractedRunnerData.Success)
                        {
                            runnerName = extractedRunnerData.Name;
                            miles = extractedRunnerData.Miles;
                            gender = extractedRunnerData.Gender;
                        }                        
                    }
                }

                int runnerId;
                try
                {
                    runnerId = FindOrCreateRunner(runnerName, gender);
                }
                catch (AmbiguousRunnerException e)
                {
                    raceParticipation.ErrorMessage = $"Ambiguous runner {e.RunnerName} entered. Add gender to be specific.";
                    return View(raceParticipation);
                }

                var raceEntry = new RaceEntry
                {
                    RunnerId = runnerId,
                    RaceId = raceId,
                    Miles = miles
                };
                var alreadyExists = ldsContext.RaceEntries
                    .Count(re => re.RunnerId == runnerId && re.RaceId == raceId) > 0;

                if (!alreadyExists)
                {
                    ldsContext.Add<RaceEntry>(raceEntry);
                    cacheInvalidations.Add(new()
                    {
                        RunnerId = runnerId.ToString(),
                        RaceId = raceId.ToString(),
                        Year = DateOnly.Parse(raceParticipation.Date).Year.ToString(),
                        Gender = gender,
                    });
                    raceEntryCreated = true;
                }
            }
            catch (Exception e)  
            {
                // TODO: log errors      
            }
            finally
            {
                ldsContext.SaveChanges();
            }
        }

        if (raceEntryCreated)
        {
            cacheInvalidation.Invalidate(cacheInvalidations);
            await parametersService.SetLastUpdated(DateTime.Now.ToUniversalTime());
            raceParticipation.Runners = "";
        }

        ViewBag.RaceParticipationByRace = GetRaceParticipationByRace(raceId);
        raceParticipation.LastAddedRace = GetLastAddedRace();
        return View(raceParticipation);
    }

    [HttpGet("ByRace/{raceId}")]
    public async Task<ActionResult<RaceParticipationByRace>> GetByRace (int raceId)
    {
        return GetRaceParticipationByRace(raceId);
    }

    [HttpGet("ByRunner/{runnerId}")]
    public async Task<ActionResult<RaceParticipationByRunner>> GetByRunner (int runnerId)
    {
        var raceParticipations = ldsContext.RacePartipation
            .Where(rp => rp.RunnerId == runnerId)
            .OrderBy(rp => rp.Date)
            .ToList();

        var raceParticipation = new RaceParticipationByRunner
        {
            RunnerId = raceParticipations.FirstOrDefault().RunnerId,
            RunnerName = raceParticipations.FirstOrDefault().RunnerName,
            Races = new List<RaceParticipationByRunnerEntry>()
        };

        raceParticipations.ForEach(rp =>
        {
            raceParticipation.Races.Add(
                new RaceParticipationByRunnerEntry
                {
                    RaceId = rp.RaceId,
                    RaceName = rp.RaceName,
                    Date = rp.Date,
                    Miles = rp.Miles     
                }
            );
        });

        return raceParticipation;
    }

    [HttpGet("TotalMiles/{gender}")]
    public async Task<ActionResult<List<TotalMiles>>> GetTotalMiles (string gender)
    {
        var x = ldsContext.TotalMiles
            .Where(tm => tm.Gender == gender)
            .OrderByDescending(tm => tm.Miles)
            .ToList();

        return x;
    }

    private Race? GetLastAddedRace()
    {
        return ldsContext.Races
            .OrderByDescending(r => r.Id)
            .FirstOrDefault();
    }
    
    private RaceParticipationByRace GetRaceParticipationByRace (int raceId)
    {
        var raceParticipations = ldsContext.RacePartipation
            .Where(rp => rp.RaceId == raceId)
            .OrderBy(rp => rp.Date)
            .ToList();

        var raceParticipation = new RaceParticipationByRace
        {
            RaceId = raceParticipations.FirstOrDefault().RaceId,
            RaceName = raceParticipations.FirstOrDefault().RaceName,
            Date = raceParticipations.FirstOrDefault().Date,
            Entries = new List<RaceParticipationByRaceEntry>()
        };

        raceParticipations.ForEach(rp =>
        {
            raceParticipation.Entries.Add(
                new RaceParticipationByRaceEntry
                {
                    RunnerId = rp.RunnerId,
                    RunnerName = rp.RunnerName,
                    Miles = rp.Miles
                }
            );
        });

        return raceParticipation;
    }

    private int FindOrCreateRace(string name, DateOnly? date)
    {
        var race = (from r in ldsContext.Races
            where r.Name == name && r.Date == date
            select r).SingleOrDefault();

        if (race == null)
        {
            var newRace = new Race
            {
                Name = name,
                Date = date
            };

            ldsContext.Add<Race>(newRace);
            ldsContext.SaveChanges();
            race = newRace;
        }
        return race.Id;
    }

    private RunnerRegexResult ExtractRunnerData (string? runnerName, RegexType type)
    {
        var extractData = new RunnerRegexResult { Success = false };

        if (!String.IsNullOrEmpty(runnerName))
        {

            string pattern = "";

            switch (type)
            {
                case RegexType.NameAndGender:
                    pattern= @"^([\w\t -]+)[ ]*\(([\w]{1})\)$";
                    break;
                case RegexType.NameAndMiles:
                    pattern= @"^([\w\t -]+)[ ]*\(([\d.]+)\)$";
                    break;
                case RegexType.NameAndGenderAndMiles:
                    pattern= @"^([\w\t -]+)[ ]*\(([\w]{1})\)[ ]*\(([\d.]+)\)$";
                    break;                    
            }
        
            Regex r = new Regex(pattern);
            Match m = r.Match(runnerName);
            if (m.Success)
            {
                extractData.Success = true;
                extractData.Name = m.Groups[1].ToString().Trim();
                switch (type)
                {
                    case RegexType.NameAndMiles:
                        extractData.Miles = Decimal.Parse(m.Groups[2].ToString());
                        break;
                    case RegexType.NameAndGender:
                        extractData.Gender = m.Groups[2].ToString().Trim();
                        break;
                    case RegexType.NameAndGenderAndMiles:
                        extractData.Gender = m.Groups[2].ToString().Trim();
                        extractData.Miles = Decimal.Parse(m.Groups[3].ToString());
                        break;                        
                }
            }
        }

        return extractData;
    }

    private int FindOrCreateRunner (string fullname, string? gender=null)
    {
        var nameParts = fullname.Trim().Split(' ');
        var lastName = nameParts.LastOrDefault();
        var firstName = string.Join(" ", nameParts.Take(nameParts.Length-1));
        Runner? runner;

        try
        {
            if (gender == null)
            {
                runner = ldsContext.Runners.SingleOrDefault(r =>
                    r.FirstName == firstName &&
                    r.LastName == lastName);
            }
            else
            {
                runner = ldsContext.Runners.SingleOrDefault(r =>
                    r.FirstName == firstName &&
                    r.LastName == lastName &&
                    r.Gender == gender);
            }
        }
        catch (Exception e)
        {
            throw new AmbiguousRunnerException(e.Message, e)
            {
                RunnerName =  fullname
            };
        }

        if (runner == null)
        {
            int? runnerId = (from a in ldsContext.RunnerAliases
                where a.Alias == fullname
                select a.RunnerId).SingleOrDefault();
                
            if (runnerId > 0)
            {
                return (int)runnerId;
            }

            var newRunner = new Runner
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = String.IsNullOrEmpty(gender) ? "" : gender
            };
            ldsContext.Add<Runner>(newRunner);
            ldsContext.SaveChanges();
            runner = newRunner;
        }
                          
        return runner.Id;

    }
}

public class RunnerRegexResult
{
    public bool Success { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public decimal Miles { get; set; }
}

public enum RegexType
{
    NameAndMiles,
    NameAndGender,
    NameAndGenderAndMiles
}