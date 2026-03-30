using LDS.Data;
using LDS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDS.Web.Admin.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController(LdsContext ldsContext) : ControllerBase
    {
        // GET: api/<RaceController>
        [HttpGet]
        public IEnumerable<Race> Get()
        {
            return ldsContext.Races.OrderByDescending(r => r.Id);
        }

    }
}
