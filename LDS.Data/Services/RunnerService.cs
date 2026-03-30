using LDS.Data.Models;
using LDS.Data.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LDS.Data.Services;

public class RunnerService(LdsContext ldsContext) : IRunnerService
{
    public Runner? Get(int id)
    {
        return ldsContext.Runners.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Runner> GetAll()
    {
        return ldsContext.Runners
            .OrderBy(r => r.LastName)
            .ThenBy(r => r.FirstName);
    }

    public IEnumerable<Runner> GetAllMissingGender()
    {
        return ldsContext.Runners
            .Where(r => r.Gender == null || String.IsNullOrEmpty(r.Gender))
            .OrderBy(r => r.LastName)
            .ThenBy(r => r.FirstName);
    }
    
    public async Task<bool> Update(int id, string firstName, string lastName, string gender)
    {
        try
        {
            var sql = @"UPDATE [Runner]
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Gender = @Gender
                        WHERE Id = @Id";

            await ldsContext.Database.ExecuteSqlRawAsync(
                sql,
                new SqlParameter("@FirstName", firstName),
                new SqlParameter("@LastName", lastName),
                new SqlParameter("@Gender", gender),
                new SqlParameter("@Id", id)
            );
        }
        catch (SqlException ex)
        {
            return false;
        }

        return true;
    }

    public void Delete(Runner runner)
    {
        ldsContext.Runners.Remove(runner);
        ldsContext.SaveChanges();
    }

    public void CreateAlias(int runnerId, string alias)
    {
        ldsContext.RunnerAliases.Add(new RunnerAlias
        {
            RunnerId = runnerId,
            Alias = alias
        });
        ldsContext.SaveChanges();
    }
}