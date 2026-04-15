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
    private IConfiguration _config;
    private readonly HttpClient _httpClient;
    
    public CacheInvalidation(IParametersService parametersService, IConfiguration config)
    {
        _parametersService = parametersService;
        _config = config;
        
        var url = config["LDS_PUBLIC_APP_URL"];
        _httpClient = new()
        {
            BaseAddress = new Uri(url!)
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