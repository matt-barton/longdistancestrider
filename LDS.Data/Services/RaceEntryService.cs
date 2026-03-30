using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LDS.Data.Services;

public class RaceEntryService (LdsContext ldsContext) : IRaceEntryService
{
    public RaceEntry? Get(int raceId, int runnerId)
    {
        return ldsContext.RaceEntries
            .FirstOrDefault(re => re.RaceId == raceId && re.RunnerId == runnerId);
    }

    public async Task<bool> UpdateMiles(int raceId, int runnerId, decimal miles)
    {
        try
        {
            const string sql = @"UPDATE [RaceEntry]
                                SET Miles = @Miles
                              WHERE RaceId = @RaceId
                                AND RunnerId = @RunnerId";

            await ldsContext.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@RaceId", raceId),
                new SqlParameter("@RunnerId", runnerId),
                new SqlParameter("@Miles", miles)
            );
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateRunner(int oldRunnerId, int newRunnerId)
    {
        try
        {
            var sql = @"UPDATE [RaceEntry]
                           SET RunnerId = @NewRunnerId
                         WHERE RunnerId = @OldRunnerId";

            await ldsContext.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@NewRunnerId", newRunnerId),
                new SqlParameter("@OldRunnerId", oldRunnerId)
            );
        }
        catch
        {
            return false;
        }
        return true;
    }
}