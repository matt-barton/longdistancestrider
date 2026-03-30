using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface IRunnerService
{
    public Runner? Get (int id);
    public IEnumerable<Runner> GetAll ();
    public IEnumerable<Runner> GetAllMissingGender ();
    public Task<bool> Update (int id, string firstName, string lastName, string gender);
    public void Delete (Runner runner);
    public void CreateAlias (int runnerId, string alias);
}