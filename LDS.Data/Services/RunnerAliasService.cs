using LDS.Data.Models;
using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class RunnerAliasService(LdsContext ldsContext) : IRunnerAliasService
{
    public IEnumerable<RunnerAlias> GetForRunner(int runnerId)
    {
        return ldsContext.RunnerAliases.Where(a => a.RunnerId == runnerId);
    }
}