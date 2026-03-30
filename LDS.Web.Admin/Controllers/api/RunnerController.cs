using LDS.Data;
using LDS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunnerController(LdsContext ldsContext) : ControllerBase
    {
        // GET: api/<RunnerController>
        [HttpGet]
        public IEnumerable<Runner> Get()
        {
            return ldsContext.Runners;
        }
    }
}
