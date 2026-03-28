using LDS.Web.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
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
