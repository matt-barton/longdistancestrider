using LDS.Data;
using LDS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceEntryController(LdsContext ldsContext) : ControllerBase
    {
        // GET: api/<RaceEntryController>
        [HttpGet]
        public IEnumerable<RaceEntry> Get()
        {
            return ldsContext.RaceEntries.OrderByDescending(r => r.RaceId);
        }
    }
}
