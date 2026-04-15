namespace LDS.Data.Services.Interfaces;

public interface IParametersService
{
    public int GetCurrentYear();
    public int GetFirstYear();
    public DateTime GetLastUpdated();
    public Task SetLastUpdated(DateTime date);
}