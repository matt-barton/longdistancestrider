using LDS.Web.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private LDSContext _ldsContext;

        public RaceController(LDSContext ldsContext)
        {
            _ldsContext = ldsContext;
        }

        // GET: api/<RaceController>
        [HttpGet]
        public IEnumerable<Race> Get()
        {
            return _ldsContext.Races.OrderByDescending(r => r.Id);
        }

    }
}
