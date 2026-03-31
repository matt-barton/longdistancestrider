using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class ParametersService (LdsContext ldsContext) : IParametersService
{
    public int GetCurrentYear()
    {
        return int.Parse(ldsContext.Parameters
            .SingleOrDefault(p => p.Name == "CurrentYear")!
            .Value);
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