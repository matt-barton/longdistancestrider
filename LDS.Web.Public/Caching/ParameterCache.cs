using LDS.Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Caching;

public interface IParameterCache
{
    public int GetCurrentYear();
    public int GetFirstYear();
    public DateTime GetLastUpdated();
}

public class ParameterCache (IParametersService parameterService, IMemoryCache cache) : IParameterCache
{
    public int GetCurrentYear()
    {
        return GetInteger("CurrentYear");
    }

    public int GetFirstYear()
    {
        return GetInteger("FirstYear");
    }

    public DateTime GetLastUpdated()
    {
        return GetDateTime("LastUpdated");
    }
    
    private int GetInteger(string name)
    {
        var cacheKey = $"Parameter_{name}";
        if (!cache.TryGetValue(cacheKey, out int value))
        {
            value = name switch
            {
                "CurrentYear" => parameterService.GetCurrentYear(),
                "FirstYear" => parameterService.GetFirstYear(),
                _ => throw new Exception($"Unknown parameter {name}")
            };
            
            cache.Set(cacheKey, value);
        }

        return value;
    }
    
    private DateTime GetDateTime(string name)
    {
        var cacheKey = $"Parameter_{name}";
        if (!cache.TryGetValue(cacheKey, out DateTime value))
        {
            value = name switch
            {
                "LastUpdated" => parameterService.GetLastUpdated(),
                _ => throw new Exception($"Unknown parameter {name}")
            };
            
            cache.Set(cacheKey, value);
        }

        return value;
    }

}