using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class ParametersService (LdsContext ldsContext) : IParametersService
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
        return int.Parse(ldsContext.Parameters
            .SingleOrDefault(p => p.Name == name)!
            .Value);
    }

    private DateTime GetDateTime(string name)
    {
        return DateTime.Parse(
            ldsContext.Parameters
                .SingleOrDefault(p => p.Name == name)!
                .Value
        );
    }
}