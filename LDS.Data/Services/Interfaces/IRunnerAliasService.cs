using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface IRunnerAliasService
{
    public IEnumerable<RunnerAlias> GetForRunner (int runnerId);
}