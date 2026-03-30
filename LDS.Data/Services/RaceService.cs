using LDS.Data.Models;
using LDS.Data.Services.Interfaces;

namespace LDS.Data.Services;

public class RaceService (LdsContext ldsContext) : IRaceService
{
    public Race? Get(int id)
    {
        return ldsContext.Races.FirstOrDefault(r => r.Id == id);
    }
}