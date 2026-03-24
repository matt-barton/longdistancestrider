using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LongDistanceStrider.Controllers.Api
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
