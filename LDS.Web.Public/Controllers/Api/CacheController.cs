using LDS.Web.Public.Caching;
using LDS.Web.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LDS.Web.Public.Controllers.Api;

[Route("Api/Cache")]
[ApiController]
public class CacheController (IMemoryCache cache, ICacheInvalidation cacheInvalidation) : Controller
{
    [HttpGet("GetKeys")]
    public List<string> GetKeys()
    {
        return ((MemoryCache)cache).Keys
            .OrderBy(key => key.ToString())
            .Select(key => key.ToString())
            .ToList()!;
    }
    
    [HttpPost("Invalidate")]
    public IActionResult Invalidate(IEnumerable<CacheInvalidationDetail> details)
    {
        try
        {
            var key = cacheInvalidation.Invalidate(details);
            return Ok(key);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("InvalidateAll")]
    public IActionResult InvalidateAll()
    {
        try
        {
            cacheInvalidation.InvalidateAll();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}