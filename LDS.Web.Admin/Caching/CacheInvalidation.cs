using LDS.Data.Services.Interfaces;
using LDS.Web.Shared.Models;

namespace LDS.Web.Admin.Caching;

public interface ICacheInvalidation
{
    public void Invalidate(IEnumerable<CacheInvalidationDetail> detail);
}
public class CacheInvalidation : ICacheInvalidation
{
    private IParametersService _parametersService;
    private HttpClient _httpClient;
    public CacheInvalidation(IParametersService parametersService)
    {
        _parametersService = parametersService;
        var url = parametersService.GetPublicAppUrl();
        _httpClient = new()
        {
            BaseAddress = new Uri(url)
        };    
    }
    
    public async void Invalidate(IEnumerable<CacheInvalidationDetail> detail)
    {
        try
        {
            await _httpClient.PostAsJsonAsync("Api/Cache/Invalidate", detail);
        }
        catch (Exception e)
        {
            // TODO: log this error
            Console.WriteLine(e);
            // and swallow the error
        }
    }
}