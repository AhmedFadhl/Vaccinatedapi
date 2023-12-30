using Microsoft.AspNetCore.Mvc;
using Vaccinatedapi.data;
using Vaccinatedapi.multiModels;

namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        dbdatacontexts _dbdatacontexts = null;

        public HomeController(dbdatacontexts dbdatacontexts)
        {
            _dbdatacontexts = dbdatacontexts;
        }

        [HttpGet]
        public async Task<ActionResult<List<homevm>>> get_advices()
        {
            homevm homevm = new homevm();
            homevm.advices = _dbdatacontexts.advices.Count();
            homevm.hospital = _dbdatacontexts.hospitals.Count();
            homevm.kids = _dbdatacontexts.kids.Count();
            homevm.vaccines = _dbdatacontexts.vaccine.Count();
            homevm.parents = _dbdatacontexts.parents.Count();

            return Ok(homevm);
        }




    }
}