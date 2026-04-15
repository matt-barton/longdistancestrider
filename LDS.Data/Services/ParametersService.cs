using LDS.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task SetLastUpdated(DateTime date)
    {
        try
        {
            var param = await ldsContext.Parameters
                .SingleAsync(p => p.Name == "LastUpdated");
            param.Value = date.ToString("yyyy-MM-dd HH:mm:ss");
            await ldsContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new DbUpdateException(e.Message);
        }
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

    private string GetString(string name)
    {
        return ldsContext.Parameters
            .SingleOrDefault(p => p.Name == name)!
            .Value;
    }
}