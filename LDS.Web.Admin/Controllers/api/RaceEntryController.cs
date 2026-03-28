using LDS.Web.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceEntryController : ControllerBase
    {
        private LDSContext _ldsContext;

        public RaceEntryController(LDSContext ldsContext)
        {
            _ldsContext = ldsContext;
        }

        // GET: api/<RaceEntryController>
        [HttpGet]
        public IEnumerable<RaceEntry> Get()
        {
            return _ldsContext.RaceEntries.OrderByDescending(r => r.RaceId);
        }
    }
}
