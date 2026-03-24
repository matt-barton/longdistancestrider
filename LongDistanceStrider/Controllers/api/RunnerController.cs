using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LongDistanceStrider.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunnerController : ControllerBase
    {
        private LDSContext _ldsContext;

        public RunnerController(LDSContext ldsContext)
        {
            _ldsContext = ldsContext;
        }

        // GET: api/<RunnerController>
        [HttpGet]
        public IEnumerable<Runner> Get()
        {
            return _ldsContext.Runners;
        }
    }
}
