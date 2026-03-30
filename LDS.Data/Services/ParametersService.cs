using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class ParametersService (LdsContext ldsContext) : IParametersService
{
    public string GetCurrentYear()
    {
        return ldsContext.Parameters
            .SingleOrDefault(p => p.Name == "CurrentYear")!
            .Value
            .ToString();
    }

    public DateTime GetLastUpdated()
    {
        return DateTime.Parse(
            ldsContext.Parameters
                .SingleOrDefault(p => p.Name == $"LastUpdated")!
                .Value
            );
    }
}