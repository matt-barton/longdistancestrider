using LDS.Data.Models;

namespace LDS.Data.Services.Interfaces;

public interface IRaceService
{
    public Race? Get(int id);

}