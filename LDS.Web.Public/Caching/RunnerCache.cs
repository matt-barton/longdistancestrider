using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Caching;

public interface IRunnerCache
{
    public Runner? Get(int id);
}
public class RunnerCache (IRunnerService runnerService, IMemoryCache cache) : IRunnerCache
{
    public Runner? Get(int id)
    {
        var cacheKey = $"Runner-{id}";
        if (!cache.TryGetValue(cacheKey, out Runner? runner))
        {
            runner = runnerService.Get(id);
            
            cache.Set(cacheKey, runner);
        }

        return runner;
    }
}